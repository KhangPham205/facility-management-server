using AutoMapper;
using backend.DTOs.Audit.Request;
using backend.DTOs.Audit.Response;
using backend.DTOs.Booking.Response;
using backend.DTOs.Borrow.Request;
using backend.DTOs.Borrow.Response;
using backend.DTOs.Building.Request;
using backend.DTOs.Building.Response;
using backend.DTOs.Criteria.Request;
using backend.DTOs.Criteria.Response;
using backend.DTOs.Equipment.Request;
using backend.DTOs.Equipment.Response;
using backend.DTOs.EquipmentCategory.Request;
using backend.DTOs.EquipmentCategory.Response;
using backend.DTOs.Floor.Request;
using backend.DTOs.Floor.Response;
using backend.DTOs.FundSource.Request;
using backend.DTOs.FundSource.Response;
using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.DTOs.Invoice.Request;
using backend.DTOs.Invoice.Response;
using backend.DTOs.Liquidate.Request;
using backend.DTOs.Liquidate.Response;
using backend.DTOs.Maintenance.Request;
using backend.DTOs.Maintenance.Response;
using backend.DTOs.Repair.Request;
using backend.DTOs.Repair.Response;
using backend.DTOs.Room.Request;
using backend.DTOs.Room.Response;
using backend.DTOs.RoomType.Request;
using backend.DTOs.RoomType.Response;
using backend.DTOs.Transfer.Request;
using backend.DTOs.Transfer.Response;
using backend.Models;
using backend.Models.Area;
using backend.Models.Audit;
using backend.Models.Borrow;
using backend.Models.EquipmentInfo;
using backend.Models.Finance;
using backend.Models.Import;
using backend.Models.Liquidate;
using backend.Models.Maintenance;
using backend.Models.Repair;
using backend.Models.Transfer;
using DTOs.ExternalUnit.Request;
using DTOs.ExternalUnit.Response;

namespace backend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ======================================================
            // 1. FACILITY (Cơ sở vật chất: Tòa, Tầng, Phòng)
            // ======================================================

            // Building
            CreateMap<CreateBuildingRequest, Building>();
            CreateMap<UpdateBuildingRequest, Building>();
            CreateMap<Building, BuildingResponse>();

            // Floor
            CreateMap<CreateFloorRequest, Floor>();
            CreateMap<UpdateFloorRequest, Floor>();
            CreateMap<Floor, FloorResponse>()
                .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.Building.BuildingName));

            // Room
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>();
            CreateMap<Room, RoomResponse>()
                .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Floor.FloorName))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.RoomType.TypeName));

            CreateMap<CreateRoomTypeRequest, RoomType>();
            CreateMap<UpdateRoomTypeRequest, RoomType>();
            CreateMap<RoomType, RoomTypeResponse>();

            // Area - Room Booking
            CreateMap<RoomBooking, RoomBookingResponse>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.RoomName))
                .ForMember(dest => dest.BorrowerName, opt => opt.MapFrom(src => src.Borrower.Fullname))
                .ForMember(dest => dest.ApprovedByName, opt => opt.MapFrom(src => src.Approver != null ? src.Approver.Fullname : null));

            // ======================================================
            // 2. EQUIPMENT & CATEGORY & CRITERIA
            // ======================================================

            CreateMap<CreateEquipmentRequest, Equipment>();
            CreateMap<UpdateEquipmentRequest, Equipment>();
            CreateMap<Equipment, EquipmentResponse>()
                .ForMember(dest => dest.EquipmentCategoryName, opt => opt.MapFrom(src => src.Category.EquipmentCategoryName))
                // Lưu ý: LocationName phải xử lý trong Service vì LocationId là dynamic (Room hoặc Kho)
                .ForMember(dest => dest.LocationName, opt => opt.Ignore());

            CreateMap<CreateEquipmentCategoryRequest, EquipmentCategory>();
            CreateMap<UpdateEquipmentCategoryRequest, EquipmentCategory>();
            CreateMap<EquipmentCategory, EquipmentCategoryResponse>();

            CreateMap<CreateCriteriaRequest, Criteria>();
            CreateMap<UpdateCriteriaRequest, Criteria>();
            CreateMap<Criteria,CriteriaResponse>();

            // ======================================================
            // 3. EXTERNAL UNIT (Nhà cung cấp)
            // ======================================================

            CreateMap<CreateExternalUnitRequest, ExternalUnit>();
            CreateMap<UpdateExternalUnitRequest, ExternalUnit>();
            CreateMap<ExternalUnit, ExternalUnitResponse>();

            // ======================================================
            // 4. IMPORT PROCESS (Quy trình Nhập)
            // ======================================================

            // -- Import Request --
            CreateMap<CreateImportRequestRequest, ImportRequest>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<ImportRequestDetailDto, ImportRequestDetail>();

            CreateMap<ImportRequest, ImportRequestResponse>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Creator.Fullname))
                .ForMember(dest => dest.ApprovedByName, opt => opt.MapFrom(src => src.Approver.Fullname));

            CreateMap<ImportRequestDetail, ImportRequestDetailResponse>();

            // -- Import Voucher --
            CreateMap<CreateImportVoucherRequest, ImportVoucher>()
                // Details cần xử lý tay trong Service vì logic nhập kho phức tạp (check tồn kho/tạo mới)
                .ForMember(dest => dest.Details, opt => opt.Ignore());

            CreateMap<ImportVoucher, ImportVoucherResponse>()
            // 1. Lấy TotalAmount từ Invoice
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src =>
                    src.Invoice != null ? src.Invoice.TotalAmount : 0))

            // 2. Lấy InvoiceId (đề phòng trường hợp InvoiceId ở bảng chính null nhưng object Invoice có data)
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src =>
                    src.InvoiceId ?? (src.Invoice != null ? src.Invoice.InvoiceId : null)))

            // 3. Lấy InvoiceNumber
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src =>
                    src.Invoice != null ? src.Invoice.InvoiceNumber : null))

            // 4. Lấy UnitId từ Invoice -> Unit
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src =>
                    src.Invoice != null && src.Invoice.Unit != null ? src.Invoice.Unit.UnitId : null))

            // 5. Lấy UnitName từ Invoice -> Unit
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src =>
                    src.Invoice != null && src.Invoice.Unit != null ? src.Invoice.Unit.UnitName : null));

            CreateMap<ImportVoucherDetailDto, ImportVoucherDetail>();

            CreateMap<ImportVoucherDetail, ImportVoucherDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.EquipmentName));

            // ======================================================
            // 5. BORROW PROCESS (Quy trình Mượn)
            // ======================================================

            CreateMap<CreateBorrowRequest, BorrowVoucher>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<BorrowDetailDto, BorrowVoucherDetail>();

            CreateMap<BorrowVoucher, BorrowVoucherResponse>()
                .ForMember(dest => dest.BorrowerName,
                           opt => opt.MapFrom(src => src.Borrower.Fullname))
                .ForMember(dest => dest.CreatedByName,
                           opt => opt.MapFrom(src => src.Creator.Fullname))
                .ForMember(dest => dest.ApprovedByName,
                           opt => opt.MapFrom(src => src.Approver != null ? src.Approver.Fullname : null));

            CreateMap<BorrowVoucherDetail, BorrowVoucherDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.EquipmentName));

            // ======================================================
            // 6. TRANSFER PROCESS (Quy trình Điều chuyển)
            // ======================================================

            CreateMap<CreateTransferRequestRequest, TransferRequest>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<TransferRequestDetailDto, TransferRequestDetail>();

            CreateMap<TransferRequest, TransferRequestResponse>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Creator.Fullname))
                .ForMember(dest => dest.ApprovedByName, opt => opt.MapFrom(src => src.Approver != null ? src.Approver.Fullname : null))
                .ForMember(dest => dest.SourceLocationName, opt => opt.Ignore())
                .ForMember(dest => dest.DestinationLocationName, opt => opt.Ignore());

            CreateMap<TransferRequestDetail, TransferRequestDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.EquipmentName));


            CreateMap<TransferVoucher, TransferVoucherResponse>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Creator.Fullname));

            CreateMap<TransferVoucherDetail, TransferVoucherDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.EquipmentName));
            // ======================================================
            // 7. MAINTENANCE PROCESS (Bảo trì)
            // ======================================================

            // -- Request --
            CreateMap<CreateMaintenanceRequestRequest, MaintenanceRequest>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<MaintenanceRequestDetailDto, MaintenanceRequestDetail>();

            CreateMap<MaintenanceRequest, MaintenanceRequestResponse>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Creator.Fullname));

            CreateMap<MaintenanceRequestDetail, MaintenanceDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.EquipmentName));

            // -- Voucher --
            CreateMap<CreateMaintenanceVoucherRequest, MaintenanceVoucher>();

            CreateMap<MaintenanceVoucher, MaintenanceVoucherResponse>()
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.Invoice.InvoiceNumber));

            // ======================================================
            // 8. REPAIR PROCESS (Sửa chữa)
            // ======================================================

            // -- Request --
            CreateMap<CreateRepairRequestRequest, RepairRequest>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<RepairRequestDetailDto, RepairRequestDetail>();

            CreateMap<RepairRequest, RepairRequestResponse>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Creator.Fullname));

            CreateMap<RepairRequestDetail, RepairRequestDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.EquipmentName));

            // -- Voucher --
            CreateMap<CreateRepairVoucherRequest, RepairVoucher>();

            CreateMap<RepairVoucher, RepairVoucherResponse>()
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.Invoice.InvoiceNumber))
                .ForMember(dest => dest.ProviderName, opt => opt.MapFrom(src => src.Provider.UnitName));

            // ======================================================
            // 9. LIQUIDATE PROCESS (Thanh lý)
            // ======================================================

            // -- Request --
            CreateMap<CreateLiquidateRequestRequest, LiquidateRequest>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));

            CreateMap<LiquidateRequestDetailDto, LiquidateRequestDetail>();

            CreateMap<LiquidateRequest, LiquidateRequestResponse>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Creator.Fullname))
                .ForMember(dest => dest.ApprovedByName, opt => opt.MapFrom(src => src.Approver.Fullname));

            CreateMap<LiquidateRequestDetail, LiquidateRequestDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src =>
                    src.Equipment != null ? src.Equipment.EquipmentName : null));

            // -- Voucher --
            CreateMap<CreateLiquidateVoucherRequest, LiquidateVoucher>()
                // Details cần map tay để xử lý logic trừ kho
                .ForMember(dest => dest.Details, opt => opt.Ignore());

            CreateMap<LiquidateVoucher, LiquidateVoucherResponse>()
            // 1. Lấy TotalAmount từ Invoice
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src =>
                    src.Invoice != null ? src.Invoice.TotalAmount : 0))

            // 2. Lấy InvoiceId (đề phòng trường hợp InvoiceId ở bảng chính null nhưng object Invoice có data)
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src =>
                    src.InvoiceId ?? (src.Invoice != null ? src.Invoice.InvoiceId : null)))

            // 3. Lấy InvoiceNumber
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src =>
                    src.Invoice != null ? src.Invoice.InvoiceNumber : null))

            // 4. Lấy UnitId từ Invoice -> Unit
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src =>
                    src.Invoice != null && src.Invoice.Unit != null ? src.Invoice.Unit.UnitId : null))

            // 5. Lấy UnitName từ Invoice -> Unit
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src =>
                    src.Invoice != null && src.Invoice.Unit != null ? src.Invoice.Unit.UnitName : null));

            CreateMap<LiquidateVoucherDetailDto, LiquidateVoucherDetail>();

            CreateMap<LiquidateVoucherDetail, LiquidateVoucherDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src =>
                    src.Equipment != null ? src.Equipment.EquipmentName : null));

            // ======================================================
            // 10. AUDIT PROCESS (Kiểm kê)
            // ======================================================

            CreateMap<CreatePeriodicAuditRequest, PeriodicAudit>();
            CreateMap<PeriodicAudit, PeriodicAuditResponse>();

            CreateMap<CreateInventoryAuditRequest, InventoryAudit>();
            CreateMap<InventoryAudit, InventoryAuditResponse>()
                // LocationName cần resolve
                .ForMember(dest => dest.LocationName, opt => opt.Ignore())
                .ForMember(dest => dest.PeriodicAuditName, opt => opt.MapFrom(src =>
                    src.PeriodicAudit != null ? src.PeriodicAudit.PeriodicAuditName : null));
            CreateMap<CreateAuditDetailRequest, AuditDetail>();
            CreateMap<UpdateAuditDetailRequest, AuditDetail>();
            CreateMap<AuditDetail, AuditDetailResponse>()
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src =>
                    src.Equipment != null ? src.Equipment.EquipmentName : null));

            // ======================================================
            // 11. FINANCE (Tài chính: FundSource & Invoice)
            // ======================================================

            // FundSource
            CreateMap<CreateFundSourceRequest, FundSource>();
            CreateMap<UpdateFundSourceRequest, FundSource>();
            CreateMap<FundSource, FundSourceResponse>();

            // Invoice
            CreateMap<CreateInvoiceRequest, Invoice>();
            CreateMap<UpdateInvoiceRequest, Invoice>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Chỉ update trường không null

            CreateMap<Invoice, InvoiceResponse>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit));
        }
    }
}