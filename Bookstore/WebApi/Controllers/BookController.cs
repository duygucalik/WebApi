using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using  System.Linq;


namespace WebApi.AddControllers{

[ApiController]
[Route("[controller]")]

public class BookController : ControllerBase
{
    private static List<Book> BookList = new List<Book>()
    {
        new Book{
            Id=1,
            Title="Leon Startup",
            GenreId=1,
            PageCount=200,
            PublishDate= new DateTime(2001,06,12)
        },
         new Book{
            Id=2,
            Title="Herland",
            GenreId=2,
            PageCount=300,
            PublishDate= new DateTime(2010,05,10)
        },
         new Book{
            Id=3,
            Title="Dune",
            GenreId=1,
            PageCount=540,
            PublishDate= new DateTime(2002,06,01)
        }
    };
    [HttpGet]  //tüm kitapları getirir
    public List<Book> GetBooks()
    {
        var bookList=BookList.OrderBy(x => x.Id).ToList<Book>();
        return bookList;
    }

    [HttpGet("{id}")] //route ile sadece id ye ait olan kitabı getirme.
    public Book GetById(int id)
    {
        var book = BookList.Where(book => book.Id == id).SingleOrDefault();
        return book;
    }

    [HttpPost] //Post ile listeye kitap ekleme işlemi yapıyoruz.
    public IActionResult AddBook([FromBody] Book newBook){
        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);

        if(book is not null) //kitap listemde varsa badrequest dönüyor
            return BadRequest();

        BookList.Add(newBook); //kitap listemde yok ise işlem başarılı.
        return Ok();
    }
    [HttpPut]
    public  IActionResult UpdateBook (int id ,[FromBody] Book updateBook)
    {
        var book =BookList.SingleOrDefault(x => x.Id == id);
        if(book is null)
           return BadRequest();

        book.GenreId= updateBook.GenreId != default ? updateBook.GenreId : book.GenreId; // eğer değişmiş ise update i al değiştirilmemişse kendi verisini al
        book.PageCount= updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
        book.PublishDate= updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
        book.Title= updateBook.Title != default ? updateBook.Title : book.Title;

        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteBook (int id)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if(book is null)
          return BadRequest();

        BookList.Remove(book);  
        return Ok();
    }

}
}