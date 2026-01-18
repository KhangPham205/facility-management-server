namespace backend.Constants
{
    public static class ApiEndpoints
    {
        //Base API Paths
        public const string ApiV1 = "api/v1";

        /**
         * Authentication
         * URI: api/v1/auth
         */
        public const string Auth = $"{ApiV1}/auth";

        /**
         * Quản lý người dùng
         * URI: api/v1/users
         */
        public const string Users = $"{ApiV1}/users";

        /**
         * Quản lý thiết bị
         * URI: api/v1/equipments
         */
        public const string Equipments = $"{ApiV1}/equipments";

        /**
         * Quản lý loại thiết bị (Categories)
         * URI: api/v1/equipment-categories
         */
        public const string EquipmentCategories = $"{ApiV1}/equipment-categories";

        /**
         * Quản lý nguồn kinh phí
         * URI: api/v1/fund-sources
         */
        public const string FundSources = $"{ApiV1}/fund-sources";

        /**
         * Quản lý phiếu mượn
         * URI: api/v1/borrow-vouchers
         */
        public const string BorrowVouchers = $"{ApiV1}/borrow-vouchers";

        /**
         * Quản lý đặt phòng
         * URI: api/v1/room-bookings
         */
        public const string RoomBookings = $"{ApiV1}/room-bookings";

        /**
         * Quản lý phiếu chuyển
         * URI: api/v1/transfer-vouchers
         */
        public const string TransferVouchers = $"{ApiV1}/transfer-vouchers";

        /**
         * Quản lý tòa
         * URI: api/v1/buildings
         */
        public const string Buildings = $"{ApiV1}/buildings";

        /**
         * Quản lý tầng
         * URI: api/v1/floors
         */
        public const string Floors = $"{ApiV1}/floors";

        /**
         * Quản lý phòng
         * URI: api/v1/rooms
         */
        public const string Rooms = $"{ApiV1}/rooms";

        /**
         * Quản lý loại phòng
         * URI: api/v1/roomTypes
         */
        public const string RoomTypes = $"{ApiV1}/roomTypes";

        /**
         * Quản lý tiêu chí
         * URI: api/v1/criteria
         */
        public const string Criterias = $"{ApiV1}/criterias";

        /**
         * Quản lý yêu cầu thanh lý
         * URI: api/v1/liquidate-requests
         */
        public const string LiquidateRequests = $"{ApiV1}/liquidate-requests";

        /**
         * Quản lý phiếu thanh lý
         * URI: api/v1/liquidate-vouchers
         */
        public const string LiquidateVouchers = $"{ApiV1}/liquidate-vouchers";

        /**
         * Quản lý phiếu yêu cầu nhập
         * URI: api/v1/import-request
         */
        public const string ImportRequests = $"{ApiV1}/import-requests";

        /**
         * Quản lý chi tiết phiếu yêu cầu nhập
         * URI: api/v1/import-request-detail
         */
        public const string ImportRequestDetails = $"{ApiV1}/import-request-detail";

        /**
         * Quản lý chi tiết phiếu nhập
         * URI: api/v1/import-voucher-detail
         */
        public const string ImportVoucherDetails = $"{ApiV1}/import-voucher-detail";

        /**
         * Quản lý chi tiết phiếu yêu cầu thanh lý
         * URI: api/v1/liquidate-request-detail
         */
        public const string LiquidateRequestDetails = $"{ApiV1}/liquidate-request-detail";

        /**
         * Quản lý chi tiết phiếu thanh lý
         * URI: api/v1/liquidate-voucher-detail
         */
        public const string LiquidateVoucherDetails = $"{ApiV1}/liquidate-voucher-detail";

        /**
         * Quản lý phiếu nhập
         * URI: api/v1/import-voucher
         */
        public const string ImportVouchers = $"{ApiV1}/import-vouchers";

        /**
         * Quản lý kiểm kê đinh kỳ
         * URI: api/v1/periodic-audit
         */
        public const string PeriodicAudits = $"{ApiV1}/periodic-audits";

        /**
         * Quản lý phiếu kiểm kê
         * URI: api/v1/inventory-audit
         */
        public const string InventoryAudits = $"{ApiV1}/inventory-audits";

        /**
         * Quản lý chi tiết phiếu kiểm kê
         * URI: api/v1/audit-detail
         */
        public const string AuditDetails = $"{ApiV1}/audit-details";

        //================================================================
        //== Whitelists cho Security (CORS / JWT Filter)
        //================================================================

        public static readonly string[] SwaggerWhitelist =
        {
            "/swagger-ui/**",
            "/swagger-ui.html",
            "/v3/api-docs/**"
        };

        public static readonly string[] PublicApiWhitelist =
        {
            $"/{Auth}/**",          // api/v1/auth/**
            "/hub/**",              // SignalR / WebSocket (nếu có)
            "/files/**"             // Static files
        };
    }
}