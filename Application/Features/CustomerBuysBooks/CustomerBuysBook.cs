using System.Net;
using Aplication.Errors;
using Aplication.Interfaces;
using Application.Features.Costumers.Specifications;
using Application.Interfaces;
using Domain;
using MediatR;
using Persistence;

namespace Application.Features.CustomerBuysBooks;

public class CustomerBuysBook
{
    public class CustomerBuysBookCommand: IRequest<CostumerBuyBook>
    {
        public int BookId { get; set; }
        public int CostumerId { get; set; }
        public int QuantityBookBought { get; set; }
    }
    
    public class CustomerBuysBookCommandHandler: IRequestHandler<CustomerBuysBookCommand,CostumerBuyBook>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAcessor _userAcessor;
        private readonly DataContext _context;

        public CustomerBuysBookCommandHandler(IUnitOfWork unitOfWork, IUserAcessor userAcessor,
            DataContext context)
        {
            _unitOfWork = unitOfWork;
            _userAcessor = userAcessor;
            _context = context;
        }
        public async Task<CostumerBuyBook> Handle(CustomerBuysBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId);
            if (book is null)
            {
                throw new RestException(HttpStatusCode.Found, "the book is not found");
            }
            
            var spec = new ListCostumerById(request.CostumerId);

            var costumer = await _unitOfWork.Repository<Costumer>().GetEntityWithSpec(spec);
            if (costumer is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Costumer Not Found");
            }
            
            var balance = book.QuantityStock;
            if (balance <= 0 && balance < request.QuantityBookBought)
            {
                throw new RestException(HttpStatusCode.Conflict, "Books in stock are lower");
            }
            
            await using var transactionScope = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                book.QuantityStock = book.QuantityStock - request.QuantityBookBought;

                _unitOfWork.Repository<Book>().Update(book);
                var totalNumberOfBooks = book.Value * request.QuantityBookBought;

                var paymentBook = new CostumerBuyBook()
                {
                    BookId = request.BookId,
                    CostumerId = request.BookId,
                    PurchaseDate = DateTime.UtcNow,
                    SaleValue = totalNumberOfBooks,
                    QuantityBookBought = request.QuantityBookBought,
                    CreatedByUserId = _userAcessor.GetCurrentUserId()

                };
            
                _unitOfWork.Repository<CostumerBuyBook>().Add(paymentBook);

                await transactionScope.CommitAsync(cancellationToken);
                var result = await _unitOfWork.Complete() <0;

                if (result)
                {
                    throw new RestException(HttpStatusCode.BadRequest, "Occurred problems for save CostumerBuyBook");
                }

                return paymentBook;
            }
            catch (Exception e)
            {
                Console.WriteLine("System Error {0}", e.Message);
                await transactionScope.RollbackAsync(cancellationToken);
                throw;
            }
            
        }
    }
}