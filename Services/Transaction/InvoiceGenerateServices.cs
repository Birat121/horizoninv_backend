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
        private readonly IInvoiceGenerateRepository _invoiceGenerateRepository;

        public InvoiceGenerateService(InvoiceGenerateRepository invoiceGenerateRepository)
        {
            _invoiceGenerateRepository = invoiceGenerateRepository;
        }

        public async Task CreateInvgAsync(CreateInvGDto dto)
        {
            var cusId = await _invoiceGenerateRepository.GetCustomerIdByNameAsync(dto.CustomerName);
            var conId = await _invoiceGenerateRepository.GetCustomerIdByNameAsync(dto.ContraName);


            if (cusId == null || conId == null)
            {
                throw new Exception("Customer or Contra not found.");
            }

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


            };
            await _invoiceGenerateRepository.AddAsync(invG);
            await _invoiceGenerateRepository.SaveChangesAsync();
        }
    }
}
