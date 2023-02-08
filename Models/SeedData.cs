using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContactBook.Data;
using System;
using System.Linq;
namespace ContactBook.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ContactBookContext(serviceProvider.GetRequiredService<DbContextOptions<ContactBookContext>>()))
            {
                //Look for any Contacts
                if (context.Contacts.Any())
                {
                    return;
                }
                context.Contacts.AddRange(
                    new Contacts 
                    { 
                        name="John",
                        surname="Smith",
                        phonenumber="599321231",
                        email="jsmith@gmail.com",
                        DOB="1992-01-02"
                    },
                    new Contacts 
                    {
                        name = "John",
                        surname = "Kowalski",
                        phonenumber = "529621451",
                        email = "jkowalski@gmail.com",
                        DOB = "1996-02-01"
                    }, 
                    new Contacts 
                    {
                        name = "Anna",
                        surname = "Foy",
                        phonenumber = "239356267",
                        email = "afoy@gmail.com",
                        DOB = "2002-12-02"
                    }, 
                    new Contacts 
                    {
                        name = "Claire",
                        surname = "Scott",
                        phonenumber = "429811145",
                        email = "cScott@gmail.com",
                        DOB = "1998-01-12"
                    }
                    );
                context.SaveChanges();

            }
        }
    }
}

