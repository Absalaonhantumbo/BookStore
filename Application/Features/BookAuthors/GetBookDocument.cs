using Application.Interfaces;
using Application.Specification.BookDocuments;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.BookAuthors;

public class GetBookDocument
{
    public class BookDocumentDto
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string FileUrl { get; set; }
    }
    
    public class GetBookDocumentQuery : IRequest<List<BookDocumentDto>>
    {
        public int BookId { get; set; }
    }
    
    public class GetBookDocumentQueryHandler : IRequestHandler<GetBookDocumentQuery, List<BookDocumentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public GetBookDocumentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<BookDocumentDto>> Handle(GetBookDocumentQuery request,
            CancellationToken cancellationToken)
        {
            var spec = new GetBookDocumentsByBookIdSpecification(request.BookId);

            var documents = await _unitOfWork.Repository<BookDocument>()
                .ListWithSpecAsync(spec);

            var items = documents.Select(x => new BookDocumentDto
            {
                BookId = x.BookId,
                Book = x.Book,
                Name = x.Name,
                Token = x.Token,
                FileUrl = GetFileUrl(x.Token, x.BookId)
            }).ToList();

            return items;

            //return _mapper.Map<List<ContractDocument>, List<ContractDocumentDto>>(documents);
        }
        
        private string GetFileUrl(string fileToken, int bookId)
        {
            var baseApiUrl = _configuration["BaseApiUrl"];
            return $"{baseApiUrl}/book/{bookId}/attachments/{fileToken}";
        }
    }

}