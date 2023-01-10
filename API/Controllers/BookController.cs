using Application.Dtos;
using Application.Features.BookAuthors;
using Application.Interfaces;
using Application.Specification.BookDocuments;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility;
using Utils = Utility.Utils;

namespace API.Controllers;

public class BookController: BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;

    public BookController(IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
    }
    
    [HttpGet("{bookId}")]
    public async Task<BookAuthorDto> GetBookByBookId(int bookId)
    {
        return await _mediator.Send(new GetBookAuthorByBookId.GetBookAuthorByBookIdQuery() {BookId = bookId});
    }
    
    [HttpPost]
    public async Task<ActionResult<BookAuthorDto>> CreateBookAuthors(CreateBookAuthors.CreateBookAuthorsCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpPost("{bookId}/upload")]
    [Consumes("multipart/form-data")]
    [AllowAnonymous]
    public async Task<IActionResult> UploadFiles(IFormFile file, [FromRoute] int bookId)
    {
        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(bookId);
    
        if (book is null)
        {
            return BadRequest("The specified BookId does not exist" );
        }
    
        try
        {
            var uploadLocalDir = _configuration["UploadDir"];
            var root = "/";
            var attachedFile = new
            {
                Id = 0,
                Name = "",
                FileUrl = "",
                FileToken = "",
                bookId = 0
            };
    
            if (uploadLocalDir[0] != '/') root = _hostEnvironment.ContentRootPath;
    
            var finalUploadDir = Path.Combine(root, uploadLocalDir);
            var ext = Path.GetExtension(file.FileName);
            var fileToken = $"{Utils.GenerateSecureString(20)}{ext}";;
    
            if (file.Length > 0)
                using (var fileStream = new FileStream(Path.Combine(finalUploadDir, fileToken), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
    
                    var bookDocument = new BookDocument();
                    bookDocument.BookId = bookId;
                    bookDocument.Token = fileToken;
                    bookDocument.CreatedAt = DateTime.UtcNow;
                    bookDocument.Name = file.FileName ;
    
                    _unitOfWork.Repository<BookDocument>().Add(bookDocument);
                    await _unitOfWork.Complete();
    
                    attachedFile = new
                    {
                        bookDocument.Id,
                        bookDocument.Name,
                        FileUrl = GetFileUrl(fileToken, bookId),
                        FileToken = fileToken,
                        bookId
                    };
                }
            return Ok(attachedFile);
        }
        catch (Exception ex)
        {
            return BadRequest("Could not upload files " + ex.Message);
        }
    }
    
    private string GetFileUrl(string fileToken, int bookId)
    {
        var baseApiUrl = _configuration["BaseApiUrl"];
        return $"{baseApiUrl}/book/{bookId}/attachments/{fileToken}";
    }
    
    [HttpGet("{bookId}/attachments/{token}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFile([FromRoute] int bookId, [FromRoute] string token)
    {
        var spec = new GetBookDocumentByBookIdAndTokwnSpecification(bookId, token);
        var document = await _unitOfWork.Repository<BookDocument>().GetEntityWithSpec(spec);

        if (document == null)
        {
            return BadRequest(new {error = "Unknown document. Probably the token is invalid."});
        }
    
        var uploadLocalDir = _configuration["UploadDir"];
        var root = "/";
    
        if (uploadLocalDir[0] != '/') root = _hostEnvironment.ContentRootPath;
    
        var finalUploadDir = Path.Combine(root, uploadLocalDir);
        var filePath = $"{finalUploadDir}/{token}";
    
        var ext = token.Substring(token.Length - 3);
        var mime = MimeTypeMap.GetMimeType(ext);
        
        if (System.IO.File.Exists(filePath)) return PhysicalFile($"{finalUploadDir}/{token}", mime);
        return NotFound(new {error = "File does not exist"});
    }
    
    [HttpGet("{bookId}/documents")]
    public async Task<List<GetBookDocument.BookDocumentDto>> GetBookDocuments(int bookId)
    {
        return await _mediator.Send(new GetBookDocument.GetBookDocumentQuery() { BookId = bookId});
    }
    
    
    
}