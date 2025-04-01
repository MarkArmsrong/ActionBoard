using Microsoft.EntityFrameworkCore;
using ActionBoard.Models;
using Microsoft.AspNetCore.Identity;

namespace ActionBoard.Data
{
    public static class DbSeeder
    {
        public static async Task SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Create default user if it doesn't exist
            var defaultUser = await userManager.FindByEmailAsync("default@example.com");
            if (defaultUser == null)
            {
                defaultUser = new ApplicationUser
                {
                    UserName = "default@example.com",
                    Email = "default@example.com",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };
                await userManager.CreateAsync(defaultUser, "DefaultPassword123!");
            }

            if (await context.Todos.AnyAsync())
            {
                return; // Database has already been seeded
            }

            var todos = new List<Todo>
            {
                // Development Tasks
                new Todo { Title = "Set up CI/CD pipeline", Description = "Configure GitHub Actions for automated testing and deployment", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-30), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Implement user authentication", Description = "Add OAuth2 authentication with support for Google and Microsoft accounts", Status = "todo", CreatedAt = DateTime.Now.AddDays(-29), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Create API documentation", Description = "Generate Swagger documentation for all API endpoints", Status = "completed", CreatedAt = DateTime.Now.AddDays(-28), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Optimize database queries", Description = "Add indexes and improve query performance for slow endpoints", Status = "todo", CreatedAt = DateTime.Now.AddDays(-27), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Write unit tests", Description = "Increase test coverage to at least 80%", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-26), IsCompleted = false, UserId = defaultUser.Id },
                
                // Design Tasks
                new Todo { Title = "Design mobile responsive layouts", Description = "Create responsive designs for all screen sizes", Status = "completed", CreatedAt = DateTime.Now.AddDays(-25), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Create style guide", Description = "Document color schemes, typography, and component usage", Status = "todo", CreatedAt = DateTime.Now.AddDays(-24), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Design dark mode theme", Description = "Create dark mode variants for all UI components", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-23), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Update icon set", Description = "Replace outdated icons with modern alternatives", Status = "completed", CreatedAt = DateTime.Now.AddDays(-22), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Improve accessibility", Description = "Ensure WCAG 2.1 compliance across all pages", Status = "todo", CreatedAt = DateTime.Now.AddDays(-21), IsCompleted = false, UserId = defaultUser.Id },
                
                // Backend Tasks
                new Todo { Title = "Implement caching layer", Description = "Add Redis caching for frequently accessed data", Status = "todo", CreatedAt = DateTime.Now.AddDays(-20), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Set up monitoring", Description = "Configure application monitoring with Prometheus and Grafana", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-19), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Optimize image processing", Description = "Implement image resizing and compression service", Status = "completed", CreatedAt = DateTime.Now.AddDays(-18), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Update dependencies", Description = "Upgrade all packages to their latest stable versions", Status = "todo", CreatedAt = DateTime.Now.AddDays(-17), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Implement rate limiting", Description = "Add API rate limiting for public endpoints", Status = "completed", CreatedAt = DateTime.Now.AddDays(-16), IsCompleted = true, UserId = defaultUser.Id },
                
                // Frontend Tasks
                new Todo { Title = "Implement state management", Description = "Add Redux for global state management", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-15), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Add loading states", Description = "Implement skeleton loading for all async operations", Status = "todo", CreatedAt = DateTime.Now.AddDays(-14), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Optimize bundle size", Description = "Reduce JavaScript bundle size through code splitting", Status = "completed", CreatedAt = DateTime.Now.AddDays(-13), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Add error boundaries", Description = "Implement error handling for all React components", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-12), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Improve form validation", Description = "Add client-side validation for all forms", Status = "todo", CreatedAt = DateTime.Now.AddDays(-11), IsCompleted = false, UserId = defaultUser.Id },
                
                // Testing Tasks
                new Todo { Title = "Write integration tests", Description = "Add end-to-end tests for critical user flows", Status = "todo", CreatedAt = DateTime.Now.AddDays(-10), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Set up test environment", Description = "Configure separate testing environment with mock data", Status = "completed", CreatedAt = DateTime.Now.AddDays(-9), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Performance testing", Description = "Conduct load testing for high-traffic scenarios", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-8), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Security testing", Description = "Perform security audit and penetration testing", Status = "todo", CreatedAt = DateTime.Now.AddDays(-7), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Cross-browser testing", Description = "Verify compatibility across major browsers", Status = "completed", CreatedAt = DateTime.Now.AddDays(-6), IsCompleted = true, UserId = defaultUser.Id },
                
                // Documentation Tasks
                new Todo { Title = "Update README", Description = "Add detailed setup and contribution guidelines", Status = "completed", CreatedAt = DateTime.Now.AddDays(-5), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Document deployment process", Description = "Create step-by-step deployment guide", Status = "todo", CreatedAt = DateTime.Now.AddDays(-4), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Create user manual", Description = "Write comprehensive user documentation", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-3), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "API versioning guide", Description = "Document API versioning strategy and migration paths", Status = "todo", CreatedAt = DateTime.Now.AddDays(-2), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Update changelog", Description = "Document all changes for the upcoming release", Status = "completed", CreatedAt = DateTime.Now.AddDays(-1), IsCompleted = true, UserId = defaultUser.Id },
                
                // Project Management Tasks
                new Todo { Title = "Sprint planning", Description = "Prepare tasks and estimates for next sprint", Status = "todo", CreatedAt = DateTime.Now.AddDays(-30), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Team retrospective", Description = "Conduct sprint retrospective meeting", Status = "completed", CreatedAt = DateTime.Now.AddDays(-29), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Resource allocation", Description = "Plan resource allocation for Q2", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-28), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Client meeting", Description = "Present project progress to stakeholders", Status = "todo", CreatedAt = DateTime.Now.AddDays(-27), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Budget review", Description = "Review and adjust project budget", Status = "completed", CreatedAt = DateTime.Now.AddDays(-26), IsCompleted = true, UserId = defaultUser.Id },
                
                // Infrastructure Tasks
                new Todo { Title = "Server maintenance", Description = "Perform routine server maintenance and updates", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-25), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Backup system", Description = "Implement automated backup solution", Status = "completed", CreatedAt = DateTime.Now.AddDays(-24), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "SSL certificate renewal", Description = "Renew SSL certificates for all domains", Status = "todo", CreatedAt = DateTime.Now.AddDays(-23), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Cloud migration", Description = "Plan migration to cloud infrastructure", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-22), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Network security", Description = "Update firewall rules and security policies", Status = "completed", CreatedAt = DateTime.Now.AddDays(-21), IsCompleted = true, UserId = defaultUser.Id },
                
                // Quality Assurance Tasks
                new Todo { Title = "Code review", Description = "Review and provide feedback on pull requests", Status = "todo", CreatedAt = DateTime.Now.AddDays(-20), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Bug triage", Description = "Categorize and prioritize reported bugs", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-19), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Regression testing", Description = "Perform regression testing for core features", Status = "completed", CreatedAt = DateTime.Now.AddDays(-18), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Performance monitoring", Description = "Set up performance monitoring alerts", Status = "todo", CreatedAt = DateTime.Now.AddDays(-17), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "User feedback review", Description = "Analyze and categorize user feedback", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-16), IsCompleted = false, UserId = defaultUser.Id },
                
                // Content Tasks
                new Todo { Title = "Content audit", Description = "Review and update website content", Status = "completed", CreatedAt = DateTime.Now.AddDays(-15), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Blog post writing", Description = "Write technical blog post about new features", Status = "todo", CreatedAt = DateTime.Now.AddDays(-14), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Email templates", Description = "Update transactional email templates", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-13), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Social media content", Description = "Create social media content calendar", Status = "completed", CreatedAt = DateTime.Now.AddDays(-12), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Help center articles", Description = "Write help center documentation", Status = "todo", CreatedAt = DateTime.Now.AddDays(-11), IsCompleted = false, UserId = defaultUser.Id },
                
                // Compliance Tasks
                new Todo { Title = "GDPR compliance", Description = "Review and update privacy policies", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-10), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Security audit", Description = "Conduct annual security audit", Status = "todo", CreatedAt = DateTime.Now.AddDays(-9), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "License review", Description = "Review and update software licenses", Status = "completed", CreatedAt = DateTime.Now.AddDays(-8), IsCompleted = true, UserId = defaultUser.Id },
                new Todo { Title = "Accessibility audit", Description = "Conduct accessibility compliance audit", Status = "inprogress", CreatedAt = DateTime.Now.AddDays(-7), IsCompleted = false, UserId = defaultUser.Id },
                new Todo { Title = "Data retention", Description = "Implement data retention policies", Status = "todo", CreatedAt = DateTime.Now.AddDays(-6), IsCompleted = false, UserId = defaultUser.Id }
            };

            await context.Todos.AddRangeAsync(todos);
            await context.SaveChangesAsync();
        }
    }
} 