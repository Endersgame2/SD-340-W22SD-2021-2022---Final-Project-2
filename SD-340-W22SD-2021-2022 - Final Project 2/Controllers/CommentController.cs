﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD_340_W22SD_2021_2022___Final_Project_2.Data;
using SD_340_W22SD_2021_2022___Final_Project_2.Models;
using SD_340_W22SD_2021_2022___Final_Project_2.Models.ViewModels;

namespace SD_340_W22SD_2021_2022___Final_Project_2.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> CommentsForTask(int ticketId)
        {
            ViewBag.ticketId = ticketId;
            CreateCommentViewModel vm;
            List<Comment>? comments;
            Comment newComment = new Comment();

            try
            {
                comments = _context.Comment
                    .Include(u => u.User)
                    .Where(c => c.TicketId == ticketId)
                    .ToList();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Project");
            }

            newComment.TicketId = ticketId;
            vm = new CreateCommentViewModel(comments, newComment);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Content, TicketId, Ticket, UserId, User")] Comment NewComment)
        {
            Comment comment = new Comment();

            try
            {
                string userName = User.Identity.Name;

                ApplicationUser user = await _userManager.FindByNameAsync(userName);

                Ticket ticket = await _context.Ticket.FindAsync(NewComment.TicketId);

                comment.TicketId = NewComment.TicketId;
                comment.Ticket = ticket;
                comment.Content = NewComment.Content;
                comment.User = user;
                comment.UserId = user.Id;

                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Project");
            }

            return RedirectToAction("CommentsForTask", new { ticketId = NewComment.TicketId });
        }
    }
}
