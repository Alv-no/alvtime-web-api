﻿using AlvTimeWebApi.Dto;
using AlvTimeWebApi.Persistence.DatabaseModels;
using System.Globalization;
using System.Linq;

namespace AlvTimeWebApi.HelperClasses
{
    public class CreatedObjectReturner
    {
        private readonly AlvTime_dbContext _database;

        public CreatedObjectReturner(AlvTime_dbContext database)
        {
            _database = database;
        }

        public CustomerDto ReturnCustomer(string customerName)
        {
            var customerResponseDto = _database.Customer
                .Where(x => x.Name == customerName)
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ContactPerson = x.ContactPerson,
                    ContactEmail = x.ContactEmail,
                    ContactPhone = x.ContactPhone,
                    InvoiceAddress = x.InvoiceAddress
                })
                .FirstOrDefault();

            return customerResponseDto;
        }

        public ProjectResponseDto ReturnCreatedProject(CreateProjectDto project)
        {
            var projectResponseDto = _database.Project
                .Where(x => x.Name == project.Name && x.Customer == project.Customer)
                .Select(x => new ProjectResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Customer = new CustomerDto
                    {
                        Id = x.CustomerNavigation.Id,
                        Name = x.CustomerNavigation.Name,
                        ContactEmail = x.CustomerNavigation.ContactEmail,
                        ContactPerson = x.CustomerNavigation.ContactPerson,
                        ContactPhone = x.CustomerNavigation.ContactPhone,
                        InvoiceAddress = x.CustomerNavigation.InvoiceAddress
                    }
                })
                .FirstOrDefault();

            return projectResponseDto;
        }

        public UserResponseDto ReturnCreatedUser(CreateUserDto user)
        {
            var userResponseDto = _database.User
                .Where(x => x.Name == user.Name && x.Email == user.Email)
                .Select(x => new UserResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    FlexiHours = x.FlexiHours,
                    StartDate = x.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                })
                .FirstOrDefault();

            return userResponseDto;
        }

        public HourRateResponseDto ReturnCreatedHourRate(CreateHourRateDto hourRate)
        {
            var taskResponseDto = _database.Task
                .Where(x => x.Id == hourRate.TaskId)
                .Select(x => new TaskResponseDto
                {
                    Description = x.Description,
                    Id = x.Id,
                    Favorite = false,
                    Locked = x.Locked,
                    Name = x.Name,
                    CompensationRate = x.CompensationRate,
                    Project = new ProjectResponseDto
                    {
                        Id = x.ProjectNavigation.Id,
                        Name = x.ProjectNavigation.Name,
                        Customer = new CustomerDto
                        {
                            Id = x.ProjectNavigation.CustomerNavigation.Id,
                            Name = x.ProjectNavigation.CustomerNavigation.Name,
                            ContactEmail = x.ProjectNavigation.CustomerNavigation.ContactEmail,
                            ContactPerson = x.ProjectNavigation.CustomerNavigation.ContactPerson,
                            ContactPhone = x.ProjectNavigation.CustomerNavigation.ContactPhone,
                            InvoiceAddress = x.ProjectNavigation.CustomerNavigation.InvoiceAddress
                        }
                    }
                })
                .FirstOrDefault();

            return _database.HourRate
                .Where(x => x.FromDate.Date == hourRate.FromDate.Date && x.Rate == hourRate.Rate && x.TaskId == hourRate.TaskId)
                .Select(x => new HourRateResponseDto
                {
                    Id = x.Id,
                    Rate = x.Rate,
                    FromDate = x.FromDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Task = taskResponseDto
                })
                .FirstOrDefault();
        }

        public TaskResponseDto ReturnTask(User user, UpdateTasksDto task)
        {
            var userHasFavorite = _database.TaskFavorites.FirstOrDefault(x => x.TaskId == task.Id && x.UserId == user.Id);

            var taskResponseDto = _database.Task
                .Where(x => x.Id == task.Id)
                .Select(x => new TaskResponseDto
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Locked = x.Locked,
                    CompensationRate = x.CompensationRate,
                    Project = new ProjectResponseDto
                    {
                        Id = x.ProjectNavigation.Id,
                        Name = x.ProjectNavigation.Name,
                        Customer = new CustomerDto
                        {
                            Id = x.ProjectNavigation.CustomerNavigation.Id,
                            Name = x.ProjectNavigation.CustomerNavigation.Name,
                            ContactEmail = x.ProjectNavigation.CustomerNavigation.ContactEmail,
                            ContactPerson = x.ProjectNavigation.CustomerNavigation.ContactPerson,
                            ContactPhone = x.ProjectNavigation.CustomerNavigation.ContactPhone,
                            InvoiceAddress = x.ProjectNavigation.CustomerNavigation.InvoiceAddress
                        }
                    },
                })
                .FirstOrDefault();

            taskResponseDto.Favorite = userHasFavorite == null ? false : true;

            return taskResponseDto;
        }

        public TaskResponseDto ReturnCreatedTask(CreateTaskDto task)
        {
            var taskResponseDto = _database.Task
                .Where(x => x.Name == task.Name && x.Project == task.Project)
                .Select(x => new TaskResponseDto
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Locked = x.Locked,
                    Favorite = false,
                    Project = new ProjectResponseDto
                    {
                        Id = x.ProjectNavigation.Id,
                        Name = x.ProjectNavigation.Name,
                        Customer = new CustomerDto
                        {
                            Id = x.ProjectNavigation.CustomerNavigation.Id,
                            Name = x.ProjectNavigation.CustomerNavigation.Name,
                            ContactEmail = x.ProjectNavigation.CustomerNavigation.ContactEmail,
                            ContactPerson = x.ProjectNavigation.CustomerNavigation.ContactPerson,
                            ContactPhone = x.ProjectNavigation.CustomerNavigation.ContactPhone,
                            InvoiceAddress = x.ProjectNavigation.CustomerNavigation.InvoiceAddress
                        }
                    }
                })
                .FirstOrDefault();

            return taskResponseDto;
        }
    }
}
