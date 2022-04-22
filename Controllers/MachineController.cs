using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XRClarkSignalR.Api.Hubs.Model;

namespace XRClarkSignalR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MachineController : ControllerBase
{
    
    private readonly XrclarkContext _context;

    
    public MachineController(XrclarkContext context)
    {
        _context = context;

    }
    
    [HttpGet("machines")]
    public async Task<ActionResult<IEnumerable<Machine>>> GetMachines()
    {
        return await _context.Machines.ToListAsync();
    }


    [HttpPost]
    public async Task<ActionResult<Machine>> PostMachine(Machine machine)
    {
        _context.Machines.Add(machine);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMachines), new { id = machine.MachineId }, machine);
    }
    
    [HttpDelete ("{id}")]
    public async Task<ActionResult<Machine>> DeleteMachine(int id)
    {
        var machine = await _context.Machines.FindAsync(id);
        if (machine == null)
        {
            return NotFound();
        }

        _context.Machines.Remove(machine);
        await _context.SaveChangesAsync();

        return machine;
    }
}