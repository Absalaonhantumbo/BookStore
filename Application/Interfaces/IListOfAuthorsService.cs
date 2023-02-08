using Application.Dtos;

namespace Application.Interfaces;

public interface IListOfAuthorsService
{
    List<AuthorBooks> GetAuthors(int bookId);
}