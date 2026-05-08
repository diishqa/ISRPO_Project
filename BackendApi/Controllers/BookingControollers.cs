using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendApi.Models;
using BackendApi.Data;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly AppDbContext _context;
    public BookingController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetAll()
    {
        var bookings = await _context.Bookings.ToListAsync();
        return Ok(bookings);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetById(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            return NotFound($"Бронь с id {id} не найдена");
        return Ok(booking);
    }
    [HttpPost("create")]
    public async Task<ActionResult<Booking>> Create([FromBody] Booking newBooking)
    {
        if (string.IsNullOrWhiteSpace(newBooking.Name))
            return BadRequest("Имя обязательно");
        if (string.IsNullOrWhiteSpace(newBooking.Phone))
            return BadRequest("Номер телефона обязательный");
        if (newBooking.Date == default)
            return BadRequest("Дата и время обязательны");
        var freeBooking = await _context.Bookings
        .FirstOrDefaultAsync(b =>
        b.TableId == newBooking.TableId &&
        b.Date == newBooking.Date &&
        b.Status != "отменено");
        if (freeBooking != null)
            return BadRequest("Стол уже занят на это врем!");
        newBooking.Status = "новое";
        _context.Bookings.Add(newBooking);
        await _context.SaveChangesAsync();
        return CreatedAtAction(
            nameof(GetById),
            new { id = newBooking.Id },
            newBooking);
    }

    [HttpGet("phone/{phone}")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetByPhone(string phone)
    {
        var bookings = await _context.Bookings
            .Where(b => b.Phone == phone && b.Status != "отменено")
            .ToListAsync();
        return Ok(bookings);
    }
    [HttpPut("cancel/{id}")]
    public async Task<ActionResult> CancelBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound("Бронь не найдена");
        }
        booking.Status = "отменено";
        await _context.SaveChangesAsync();
        return Ok(new
        {
            message = "Бронь отменена"
        });
    }
}