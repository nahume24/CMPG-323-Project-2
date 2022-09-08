using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_32210310.Models;
using Microsoft.AspNetCore.JsonPatch;
using JWTAuthentication.Authentication;

namespace Project2_32210310.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]

    [Route("api/[controller]")]
    [ApiController]
    
    public class DevicesController : ControllerBase
    {
        private readonly _32210310Project2Context _context;

        public DevicesController(_32210310Project2Context context)
        {
            _context = context;
        }

        // GET: api/Devices- all Device entries from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevice()
        {
            return await _context.Device.ToListAsync();
        }

        // GET: api/Devices/5- Device from the database based on the ID parsed through
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(Guid id)
        {
            var device = await _context.Device.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }
 
        // POST: api/Devices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            _context.Device.Add(device);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DeviceExists(device.DeviceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
        }

        //Patch
        [HttpPatch("{id}")] //update or
        private async Task<IActionResult> PatchDevice(Guid id, Device device)
        {
            if (id != device.DeviceId)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Device>> DeleteDevice(Guid id)
        {
            var device = await _context.Device.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Device.Remove(device);
            await _context.SaveChangesAsync();

            return device;
        }

        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }
        //a private method in the API that checks if a Device exists


        // PUT: api/Devices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")] //update or
        private async Task<IActionResult> PutDevice(Guid id, Device device)
        {
            if (id != device.DeviceId)
           {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
           }
            catch (DbUpdateConcurrencyException)
           {
                if (!DeviceExists(id))
                {
                   return NotFound();
               }
                else
                {
                   throw;
               }
           }

            return NoContent();
        }
    }
}
