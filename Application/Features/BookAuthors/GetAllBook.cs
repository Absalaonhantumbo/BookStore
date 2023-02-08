using Application.Dtos;
using Application.Features.BookAuthors.Specifications;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.BookAuthors;

public class GetAllBook
{
    public class GetAllBookQuery: IRequest<List<BookAuthorDto>>
    {
        
    }
    
    public class GetAllBookQueryHandler: IRequestHandler<GetAllBookQuery, List<BookAuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBookQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<BookAuthorDto>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        private List<AuthorBooks> GetAuthors(int bookId)
        {
            var authorSpecification = new ListBookAuthorByBookIdSpecification(bookId);
            var authors =  _unitOfWork.Repository<AuthorBook>()
                .ListWithSpecAsync(authorSpecification).GetAwaiter().GetResult();

            var authorList = authors.Select(author => new AuthorBooks()
            {
                Id = author.AuthorId,
            }).ToList();

            return authorList;
        }
    }
}