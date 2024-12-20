using System.ComponentModel.DataAnnotations;
using Abstracts;
using AutoMapper;
using Configuration;
using Entities;
using Entities.DTOs;
using Entities.Exceptions;
using Repositories;

namespace Services;

public class BookServiceV3 : IBookService
{
    private readonly BookRepository _bookRepo;
    private readonly IMapper _mapper;

    public BookServiceV3(BookRepository bookRepo, IMapper mapper)
    {
        _bookRepo = bookRepo;
        _mapper = mapper;
    }

    public int Count => _bookRepo.GetAll().Count;

    public Book AddBook(BookDtoForInsertion item)
    {
        // Generic validation
        Validate(item);

        var book = _mapper.Map<Book>(item);
        _bookRepo.Add(book);
        return book;
    }

    public void DeleteBook(int id)
    {
        id.ValidateIdRange();
        var book = _bookRepo.Get(id);
        if (book is not null)
            _bookRepo.Remove(book);
        else
            throw new BookNotFoundException(id);
    }

    public Book? GetBookById(int id)
    {
        id.ValidateIdRange();
        var book = _bookRepo.Get(id);

        return book is not null
            ? book
            : throw new BookNotFoundException(id);
    }


    public List<Book> GetBooks() =>
        _bookRepo.GetAll();


    public Book UpdateBook(int id, BookDtoForUpdate item)
    {
        id.ValidateIdRange();

        // Generic validation
        Validate(item);

        var book = _bookRepo.Get(id);

        book = _mapper.Map(item, book);
        _bookRepo.Update(book);
        return book;
    }

    private void Validate<T>(T item)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(item);
        var isValid = Validator.TryValidateObject(item, context, validationResults, true);

        if (!isValid)
        {
            var errors = string.Join(" ", validationResults.Select(v => v.ErrorMessage));
            throw new ValidationException(errors);
        }
    }
}
