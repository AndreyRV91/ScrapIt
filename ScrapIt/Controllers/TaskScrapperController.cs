using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScrapIt.Domain.Contracts;
using ScrapIt.Domain.Contracts.Models;

namespace ScrapIt.Web.Controllers
{
    /// <summary>
    /// REST API tasks for Avito
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TaskScrapperController : ControllerBase
    {
        private readonly ILogger<TaskScrapperController> _logger;
        private readonly ITaskScrapperService _taskScrapperService;

        public TaskScrapperController(ILogger<TaskScrapperController> logger, ITaskScrapperService taskScrapperService)
        {
            _logger = logger;
            _taskScrapperService = taskScrapperService;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskScrapperService.GetAll();

            return Ok(tasks);
        }

        /// <summary>
        /// Get all tasks by id
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var task = await _taskScrapperService.GetById(id);

            if(task == null)
            {
                return BadRequest("Task was not found");
            }

            return Ok(task);
        }

        /// <summary>
        /// Create new task for Avito
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateDto taskModel)
        {
            var taskId = await _taskScrapperService.Create(taskModel);

            return Ok(taskId);
        }

        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(TaskDto taskDto)
        {
            await _taskScrapperService.Remove(taskDto);

            return Ok();
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(TaskDto taskDto)
        {
            await _taskScrapperService.Update(taskDto);

            return Ok();
        }
    }
}
