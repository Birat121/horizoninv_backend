using backend.DTOs;
using backend.Models;
using backend.Repository.Transaction;

namespace backend.Services.Transaction
{
    public interface IInvoiceGenerateService
    {
        Task CreateInvgAsync(CreateInvGDto dto);
    }

    public class InvoiceGenerateService : IInvoiceGenerateService
    {
        private readonly IInvoiceGenerateRepository _iinvoiceGenerateRepository;

        public InvoiceGenerateService(IInvoiceGenerateRepository invoiceGenerateRepository)
        {
            _iinvoiceGenerateRepository = invoiceGenerateRepository;
        }

        public async Task CreateInvgAsync(CreateInvGDto dto)
        {
            try
            {
                // Basic DTO validation
                if (string.IsNullOrWhiteSpace(dto.CustomerName))
                    throw new Exception("Customer name is required.");

                if (string.IsNullOrWhiteSpace(dto.ContraName))
                    throw new Exception("Contra name is required.");

                if (dto.NetAmt <= 0)
                    throw new Exception("Net amount must be greater than 0.");

                // Look up Customer and Contra IDs
                var cusId = await _iinvoiceGenerateRepository.GetCustomerIdByNameAsync(dto.CustomerName);
                if (cusId == null)
                    throw new Exception($"Customer '{dto.CustomerName}' not found.");

                var conId = await _iinvoiceGenerateRepository.GetCustomerIdByNameAsync(dto.ContraName);
                if (conId == null)
                    throw new Exception($"Contra account '{dto.ContraName}' not found.");

                // Create invoice entity
                var invG = new InvG
                {
                    CusId = cusId,
                    ConId = conId,
                    vRef = dto.vRef,
                    DocNo = dto.DocNo,
                    SubTotal = dto.SubTotal,
                    DiscAmt = dto.DiscAmt,
                    DiscPer = dto.DiscPer,
                    VatAmt = dto.VatAmt,
                    NetAmt = dto.NetAmt,
                    gTotal = dto.gTotal,
                    TransDate = dto.TransDate,
                    EntSysDate = DateTime.Now,
                    PanNo = dto.PanNo,
                    AmtWord = NumberToWordsConverter.Convert(dto.gTotal)

                };

                // Save to database
                await _iinvoiceGenerateRepository.AddAsync(invG);
                await _iinvoiceGenerateRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Optional: log exception
                throw new Exception("Failed to create invoice: " + ex.Message);
            }
        }
    }
}
