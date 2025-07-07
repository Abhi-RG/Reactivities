using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistance;

namespace Application.Activities.Commands
{
    public class EditActivity
    {
        public class Command : IRequest<string>
        {
            public required Activity Activity { get; set; }

        }
        public class Handler(AppDbContext context) : IRequestHandler<Command, string>
        {
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken)
                    ?? throw new Exception("Activity not found");

                activity.Title = request.Activity.Title;
              
                await context.SaveChangesAsync(cancellationToken);
                return activity.Id.ToString();
            }
        }
    }
}