using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace StackServer.Controllers
{
    [Route("api")]
    public class StackController : Controller
    {
        private static readonly Dictionary<Guid, Stack<int>> stacks = 
                            new Dictionary<Guid, Stack<int>>();
        
        private Stack<int> FindOrCreateStack(Guid id)
        {
            if (!stacks.ContainsKey(id))
                stacks.Add(id, new Stack<Int32>());

            return stacks[id];
        }

        [HttpGet("pop")]
        public IActionResult Pop(Guid id)
        {
            if (!ModelState.IsValid || Guid.Empty == id)
                return BadRequest();

            Stack<int> stack = FindOrCreateStack(id);
            
            if (stack.Count == 0)
                return NoContent();
            
            return new ObjectResult(stack.Pop());
        }
        
        [HttpGet("push")]
        public IActionResult Push(Guid id, int value)
        {
            if (!ModelState.IsValid || Guid.Empty == id)
                return BadRequest();

            Stack<int> stack = FindOrCreateStack(id);

            stack.Push(value);
            return Ok();
        }

        [HttpGet("view")]
        public IActionResult View(Guid id)
        {
            if (!ModelState.IsValid || Guid.Empty == id)
                return BadRequest();

            Stack<int> stack = FindOrCreateStack(id);

            return new ObjectResult(stack.ToArray());
        }
        
    }
}
