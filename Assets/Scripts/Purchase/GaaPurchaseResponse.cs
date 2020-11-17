using System;
using System.Text;

namespace Gaa
{
	[Serializable]
	public class PurchaseResponse
	{
		public IapResult iapResult;
        public PurchaseData purchaseData;
        public string signature;
        public int count;				  // totalCount중 현재 count로 범위는 1 ~ tatalCount 이다.
        public int totalCount;			  // PurchaseData의 총 개수로 해당 숫자만큼 콜백이 불리게 된다.

        public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("iapResult: " + iapResult + "\n");
			sb.Append ("purchaseData: " + purchaseData + "\n");
			sb.Append ("signature: " + signature + "\n");
            sb.Append("count: " + count + "\n");
            sb.Append("totalCount: " + totalCount + "\n");
            return sb.ToString();
		}
	}

	[Serializable] 
	public class Signature
	{
		public string signature;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append (signature);
			return sb.ToString();
		}
	}

	[Serializable]
	public class ProductDetailResponse
	{
		public IapResult iapResult;
		public ProductDetail productDetail;
		public int count;
        public int totalCount;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("iapResult: " + iapResult + "\n");
			sb.Append ("productDetail: " + productDetail + "\n");
            sb.Append("count: " + count + "\n");
            sb.Append("totalCount: " + totalCount + "\n");
            return sb.ToString();
		}
	}

	[Serializable]
	public class RecurringResponse
    {
		public IapResult iapResult;
		public PurchaseData purchaseData;
		public string signature;
		public string action;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("iapResult: " + iapResult + "\n");
			sb.Append("purchaseData: " + purchaseData + "\n");
			sb.Append("signature: " + signature + "\n");
			sb.Append("action: " + action + "\n");
			return sb.ToString();
		}
	}

	[Serializable]
	public class ProductDetail
	{
		public string productId;
		public string type;
		public string price;
		public string priceAmountMicros;
		public string priceCurrencyCode;
		public string title;

        public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append ("productId: " + productId + "\n");
			sb.Append ("type: " + type + "\n");
			sb.Append ("price: " + price + "\n");
			sb.Append ("priceAmountMicros: " + priceAmountMicros + "\n");
			sb.Append ("priceCurrencyCode: " + priceCurrencyCode + "\n");
			sb.Append ("title: " + title + "\n");
            return sb.ToString();
		}
	}

	[Serializable]
	public class PurchaseData
	{
		public string orderId;
		public string packageName;
		public string productId;
		public long purchaseTime;
		public string purchaseId;
		public string purchaseToken;
		public string developerPayload;
		public int purchaseState;
		public int recurringState;
		public int acknowledgeState;

		public bool IsAcknowledged()
        {
			return acknowledgeState == AcknowledgeState.ACKNOWLEDGED;

		}
		
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder ("[Product]\n");
			sb.Append ("orderId: " + orderId + "\n");
			sb.Append ("packageName: " + packageName + "\n");
			sb.Append ("productId: " + productId + "\n");
			sb.Append ("purchaseToken: " + purchaseToken + "\n");
			sb.Append ("purchaseTime: " + purchaseTime + "\n");
			sb.Append ("purchaseId: " + purchaseId + "\n");
			sb.Append ("developerPayload: " + developerPayload + "\n");
			sb.Append ("purchaseState: " + purchaseState + "\n");
			sb.Append ("recurringState: " + recurringState + "\n");
			sb.Append ("acknowledgeState: " + acknowledgeState + "\n");

			return sb.ToString();
		}
	}

	[Serializable]
	public class IapResultResponse
	{
		public IapResult iapResult;

		public override string ToString()
		{
			return iapResult.ToString();
		}
	}

    [Serializable]
    public class IapResult
    {
        public int code;
        public string message;

		public bool IsSuccess()
		{
			return code == ResponseCode.RESULT_OK;
		}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("code: " + code + ", message: " + message + "\n");
            return sb.ToString();
        }
    }

	[Serializable]
	public class PurchaseFlowParams
	{
		public string productId;
		public string productType;
		public string productName;
		public string devPayload;
		public string gameUserId;
		public bool promotionApplicable;
	}

	public class ResponseCode
	{
		/* api response code
         *        
     	 * RESULT_OK = "Success"
     	 * RESULT_USER_CANCELED = "The payment has been canceled."
     	 * RESULT_SERVICE_UNAVAILABLE = "A device or server network error has occurred."
     	 * RESULT_BILLING_UNAVAILABLE = "An error occurred during the purchase process."
     	 * RESULT_ITEM_UNAVAILABLE = "The product is not on sale or cannot be purchased."
     	 * RESULT_DEVELOPER_ERROR = "Invalid request."
     	 * RESULT_ERROR = "An unknown error has occurred."
     	 * RESULT_ITEM_ALREADY_OWNED = "The item has already been provided."
     	 * RESULT_ITEM_NOT_OWNED = "Can't consume it. Please check whether the item has been paid or not."
     	 * RESULT_FAIL = "Payment failed. Please check the payment availability and payment method and make a payment again."
     	 * RESULT_NEED_LOGIN = "Available after signing in the store app."
     	 * RESULT_NEED_UPDATE = "Payment requested by abnormal app."
     	 * RESULT_SECURITY_ERROR = "Payment has been requested from an untrusted app."
     	 * RESULT_BLOCKED_APP = "The request was blocked."
     	 * RESULT_NOT_SUPPORT_SANDBOX = "Feature not supported in test environment."
     	 * RESULT_EMERGENCY_ERROR = "Maintenance in progress."
     	 * ERROR_DATA_PARSING = "An error occurred while parsing response data."
     	 * ERROR_SIGNATURE_VERIFICATION = "A signature verification error has occurred."
     	 * ERROR_ILLEGAL_ARGUMENT = "Parameter is invalid."
     	 * ERROR_UNDEFINED_CODE = "An unknown error has occurred."
     	 * ERROR_SIGNATURE_NOT_VALIDATION = "The license key is invalid."
     	 * ERROR_UPDATE_OR_INSTALL = "Installation of payment module failed."
     	 * ERROR_SERVICE_DISCONNECTED = "The connection to the payment module has been lost."
     	 * ERROR_FEATURE_NOT_SUPPORTED = "This feature is not supported."
     	 * ERROR_SERVICE_TIMEOUT = "Timed out."
         */
        public const int RESULT_OK = 0;
        public const int RESULT_USER_CANCELED = 1;
        public const int RESULT_SERVICE_UNAVAILABLE = 2;
        public const int RESULT_BILLING_UNAVAILABLE = 3;
        public const int RESULT_ITEM_UNAVAILABLE = 4;
        public const int RESULT_DEVELOPER_ERROR = 5;
        public const int RESULT_ERROR = 6;
        public const int RESULT_ITEM_ALREADY_OWNED = 7;
        public const int RESULT_ITEM_NOT_OWNED = 8;
        public const int RESULT_FAIL = 9;
        public const int RESULT_NEED_LOGIN = 10;
        public const int RESULT_NEED_UPDATE = 11;
        public const int RESULT_SECURITY_ERROR = 12;
        public const int RESULT_BLOCKED_APP = 13;
        public const int RESULT_NOT_SUPPORT_SANDBOX = 14;

        public const int RESULT_EMERGENCY_ERROR = 99999;

        public const int ERROR_DATA_PARSING = 1001;
        public const int ERROR_SIGNATURE_VERIFICATION = 1002;
        public const int ERROR_ILLEGAL_ARGUMENT = 1003;
        public const int ERROR_UNDEFINED_CODE = 1004;
        public const int ERROR_SIGNATURE_NOT_VALIDATION = 1005;
        public const int ERROR_UPDATE_OR_INSTALL = 1006;
        public const int ERROR_SERVICE_DISCONNECTED = 1007;
        public const int ERROR_FEATURE_NOT_SUPPORTED = 1008;
        public const int ERROR_SERVICE_TIMEOUT = 1009;
	}

	public class RecurringAction
    {
		public const string REACTIVATE = "reactivate";
		public const string CANCEL = "cancel";
	}

	public class RecurringState
	{
        public const int NON_AUTO_PRODUCT = -1;  // 상품타입이 자동결제가 아닌 경우
		public const int RECURRING = 0;          // 자동 결제중
        public const int CANCEL = 1;             // 해지 예약중
	}

	public class AcknowledgeState
	{
		public const int UNSPECIFIED_STATE = -1; // 명시되지 않은 상태값
        public const int NOT_ACKNOWLEDGED = 0;	 // 확인되지 않음
        public const int ACKNOWLEDGED = 1;		 // 확인됨
	}

	public class ProductType
    {
		public const string INAPP = "inapp";
		public const string AUTO = "auto";
		public const string ALL = "all";
    }
}


