// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using ItechCleanArst.Application.Interfaces;
// using ItechCleanArst.Domain.Entities;
// using MediatR;

// namespace ItechCleanArst.Application.Bussines.Books.Commands.BookAuthors
// {
//     public class CreateBookAuthorCommandHandler : IRequestHandler<CreateBookAuthorCommand, Guid>
//     {
//         private readonly IApplicationDbContext _dbcontext;

//         public CreateBookAuthorCommandHandler(IApplicationDbContext dbcontext)
//         {
//             _dbcontext = dbcontext;
//         }
//         public async Task<Guid> Handle(CreateBookAuthorCommand request, CancellationToken cancellationToken)
//         {
//             var bookAuthor = new List<BookAuthor>();
//             foreach (var item in request.AuthorId)
//             {
//                 bookAuthor.Add(new BookAuthor
//                 {
//                     Id = Guid.NewGuid(),
//                     BookId = request.BookId,
//                     AuthorId = item
//                 });
//             }

//             _dbcontext.BookAuthors.AddRange(bookAuthor);
//             await _dbcontext.SaveChangesAsync(cancellationToken);

//             return request.BookId;
//         }
//     }
// }