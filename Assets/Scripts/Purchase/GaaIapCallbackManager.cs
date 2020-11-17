using System;
using UnityEngine;
using Gaa;

public class GaaIapCallbackManager : MonoBehaviour
{
    public static event Action<bool> ServiceAvailableEvent;
    public static event Action<IapResult> PurchaseClientStateEvent;

    public static event Action<PurchaseData, string, int, int> PurchaseUpdatedSuccessEvent;
    public static event Action<IapResult> PurchaseUpdatedErrorEvent;

    public static event Action<ProductDetail, int, int> ProductDetailsSuccessEvent;
    public static event Action<IapResult> ProductDetailsErrorEvent;

	public static event Action<PurchaseData> ConsumeSuccessEvent;
	public static event Action<IapResult> ConsumeErrorEvent;

	public static event Action<PurchaseData> AcknowledgeSuccessEvent;
	public static event Action<IapResult> AcknowledgeErrorEvent;

	public static event Action<PurchaseData, string> RecurringSuccessEvent;
	public static event Action<IapResult> RecurringErrorEvent;

    public static event Action<string> StoreInfoEvent;

	public static event Action<IapResult> LoginFlowEvent;
    public static event Action<IapResult> UpdateOrInstallFlowEvent;


    /**
     * startConnection의 결과를 제공받기 위한 콜백입니다.
     * IapResult.code 값으로 상태를 확인할 수 있습니다.
     */
    public void PurchaseClientStateListener(string result)
    {
        IapResultResponse res = JsonUtility.FromJson<IapResultResponse>(result);
        IapResult iapResult = res.iapResult;

        ServiceAvailableEvent(iapResult.IsSuccess());
        PurchaseClientStateEvent(iapResult);
    }

    /**
     * launchPurchaseFlow 호출에 의한 응답 함수 입니다.
     */
    public void PurchaseUpdatedListener(string result)
    {
        SendPurchaseUpdatedData(result);
    }

    /**
     * queryPurchases 호출에 의한 응답 함수 입니다.
     */
    public void QueryPurchaseListener(string result)
    {
        SendPurchaseUpdatedData(result);
    }

    private void SendPurchaseUpdatedData(string result)
    {
        try
        {
            PurchaseResponse purchaseRes = JsonUtility.FromJson<PurchaseResponse>(result);
            IapResult iapResult = purchaseRes.iapResult;
            if (iapResult.IsSuccess())
            {
                if (purchaseRes.totalCount > 0)
                {
                    PurchaseData purchaseData = purchaseRes.purchaseData;
                    string signature = purchaseRes.signature;
                    int count = purchaseRes.count;
                    int totalCount = purchaseRes.totalCount;
                    PurchaseUpdatedSuccessEvent(purchaseData, signature, count, totalCount);
                }
            }
            else
            {
                PurchaseUpdatedErrorEvent(iapResult);
            }
        }
        catch (System.Exception ex)
        {
            IapResult errorResult = new IapResult
            {
                code = ResponseCode.ERROR_DATA_PARSING,
                message = ex.StackTrace
            };
            PurchaseUpdatedErrorEvent(errorResult);
        }
    }

    /**
     * 인앱 상품정보를 조회에 대한 응답 함수 입니다.
     */
    public void ProductDetailsListener(string result)
    {
        try {
            ProductDetailResponse detailRes = JsonUtility.FromJson<ProductDetailResponse>(result);
            IapResult iapResult = detailRes.iapResult;
            if (iapResult.IsSuccess())
            {
                ProductDetail productDetail = detailRes.productDetail;
                int count = detailRes.count;
                int totalCount = detailRes.totalCount;
                ProductDetailsSuccessEvent(productDetail, count, totalCount);
            }
            else
            {
                // IapResult.code 값으로 상태를 확인할 수 있습니다.
                ProductDetailsErrorEvent(iapResult);
            }
        }
        catch(System.Exception ex)
        {
            IapResult errorResult = new IapResult
            {
                code = ResponseCode.ERROR_DATA_PARSING,
                message = ex.StackTrace
            };
            ProductDetailsErrorEvent(errorResult);
        }
    }


    /**
   	 * 상품소비에 대한 응답 함수 입니다.
	 */
    public void ConsumeListener (string result)
	{
        try {
            PurchaseResponse purchaseRes = JsonUtility.FromJson<PurchaseResponse>(result);
            IapResult iapResult = purchaseRes.iapResult;
            if (iapResult.IsSuccess())
            {
                PurchaseData purchaseData = purchaseRes.purchaseData;
                ConsumeSuccessEvent(purchaseData);
            }
            else
            {
                // IapResult.code 값으로 상태를 확인할 수 있습니다.
                ConsumeErrorEvent(iapResult);
            }
        }
        catch(System.Exception ex)
        {
            IapResult errorResult = new IapResult
            {
                code = ResponseCode.ERROR_DATA_PARSING,
                message = ex.StackTrace
            };
            ConsumeErrorEvent(errorResult);
        }
	}

    /**
	 * 상품확인에 대한 응답 함수 입니다.
	 */
    public void AcknowledgeListener(string result)
    {
        try {
            PurchaseResponse purchaseRes = JsonUtility.FromJson<PurchaseResponse>(result);
            IapResult iapResult = purchaseRes.iapResult;
            if (iapResult.IsSuccess())
            {
                PurchaseData purchaseData = purchaseRes.purchaseData;
                AcknowledgeSuccessEvent(purchaseData);
            }
            else
            {
                // IapResult.code 값으로 상태를 확인할 수 있습니다.
                AcknowledgeErrorEvent(iapResult);
            }
        }
        catch(System.Exception ex)
        {
            IapResult errorResult = new IapResult
            {
                code = ResponseCode.ERROR_DATA_PARSING,
                message = ex.StackTrace
            };
            AcknowledgeErrorEvent(errorResult);
        }
    }

    /*
     * 월정액상품(auto)의 상태변경(해지예약 / 해지예약 취소)에 대한 응답 함수 입니다.
     */
    public void RecurringProductListener (string result)
	{
        try {
            RecurringResponse recurringRes = JsonUtility.FromJson<RecurringResponse>(result);
            IapResult iapResult = recurringRes.iapResult;
            if (iapResult.IsSuccess())
            {
                PurchaseData purchaseData = recurringRes.purchaseData;
                string action = recurringRes.action;
                RecurringSuccessEvent(purchaseData, action);
            }
            else
            {
                // IapResult.code 값으로 상태를 확인할 수 있습니다.
                RecurringErrorEvent(iapResult);
            }
        }
        catch(System.Exception ex)
        {
            IapResult errorResult = new IapResult
            {
                code = ResponseCode.ERROR_DATA_PARSING,
                message = ex.StackTrace
            };
            RecurringErrorEvent(errorResult);
        }
    }

    public void StoreInfoListener(string result)
    {
        StoreInfoEvent(result);
    }

    /**
     * Purchase service의 로그인 요청에 대한 응답 함수 입니다.
     */
    public void LoginFlowListener (string result)
	{
        try {
            IapResultResponse res = JsonUtility.FromJson<IapResultResponse>(result);
            LoginFlowEvent(res.iapResult);
        }
        catch(System.Exception ex)
        {
            IapResult errorResult = new IapResult
            {
                code = ResponseCode.ERROR_DATA_PARSING,
                message = ex.StackTrace
            };
            LoginFlowEvent(errorResult);
        }
    }

    /**
     * Purchase service의 업데이트 또는 설치에 대한 응답 함수 입니다.
     */
    public void UpdateOrInstallListener(string result)
    {
        try {
            IapResultResponse res = JsonUtility.FromJson<IapResultResponse>(result);
            UpdateOrInstallFlowEvent(res.iapResult);
        }
        catch(System.Exception ex)
        {
            IapResult errorResult = new IapResult
            {
                code = ResponseCode.ERROR_DATA_PARSING,
                message = ex.StackTrace
            };
            UpdateOrInstallFlowEvent(errorResult);
        }
    }
}
