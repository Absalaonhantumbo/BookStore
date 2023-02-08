using Application.Dtos;
using Application.Features.BookAuthors.Specifications;
using Application.Interfaces;
using Domain;

namespace Infrastruture.Services;

public class ListOfAuthorsService: IListOfAuthorsService
{
    private readonly IUnitOfWork _unitOfWork;

    public ListOfAuthorsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<AuthorBooks> GetAuthors(int bookId)
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