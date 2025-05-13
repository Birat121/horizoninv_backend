using backend.Data;
using backend.Repository.InventoryMasterRepository;
using backend.Repository.Transaction;
using backend.Services;
using backend.Services.InventoryMasterServices;
using backend.Services.Transaction;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<ICustomerServices,CustomerServices>();

builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IVendorService, VendorService>();

builder.Services.AddScoped<IBgEntryRepository, BgEntryRepository>();
builder.Services.AddScoped<IBgEntryServices, BgEntryServices>();

builder.Services.AddScoped<IPdcEntryRepository, PdcEntryRepository>();
builder.Services.AddScoped<IPdcEntryServices, PdcEntryServices>();

// Repository registration
builder.Services.AddScoped<IInvoiceGenerateRepository, InvoiceGenerateRepository>();

// Service registration
builder.Services.AddScoped<IInvoiceGenerateService, InvoiceGenerateService>();


builder.Services.AddScoped<IMaterialIssueNoteRepository, MaterialIssueNoteRepository>();
builder.Services.AddScoped<IMaterialIssueNoteServices, MaterialIssueNoteServices>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();

builder.Services.AddScoped<ICsvImportService, CsvImportService>();

builder.Services.AddScoped<IJournalVoucherRepository, JournalVoucherRepository>();
builder.Services.AddScoped<IJournalVoucherService, JournalVoucherService>();


// 👇 ADD THIS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 👇 ADD THIS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

