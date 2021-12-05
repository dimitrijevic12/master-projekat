import {
  GET_PSPREQUEST,
  GET_PSPREQUEST_ERROR,
  POST_TRANSACTION,
  POST_TRANSACTION_ERROR,
} from "../types/types";
import axios from "axios";

export const getPSPRequest = (paymentId) => async (dispatch) => {
  try {
    debugger;
    const response = await axios.get(
      `${process.env.REACT_APP_API_URL}psprequests`,
      {
        params: { paymentId: paymentId },

        headers: {
          "Access-Control-Allow-Origin": "*",
          //Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      }
    );
    debugger;
    dispatch({
      type: GET_PSPREQUEST,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_PSPREQUEST_ERROR,
      payload: console.log(e),
    });
  }
};

export const postTransaction = (cardInfo) => async (dispatch) => {
  try {
    debugger;
    const response = await axios.post(
      `${process.env.REACT_APP_API_URL}Transactions`,
      {
        PaymentId: cardInfo.PaymentId,
        PAN: cardInfo.PAN,
        SecurityCode: cardInfo.SecurityCode,
        CardHolderName: cardInfo.CardHolderName,
        ExpirationDate: cardInfo.ExpirationDate,
        Amount: cardInfo.Amount,
      },
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          //Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
        },
      }
    );
    debugger;
    window.location.replace(cardInfo.successUrl);
    dispatch({
      type: POST_TRANSACTION,
      payload: response.data,
    });
  } catch (e) {
    debugger;
    if (e.response.data.transactionStatus === "Failed")
      window.location.replace(cardInfo.failedUrl);
    else window.location.replace(cardInfo.errorUrl);
    dispatch({
      type: POST_TRANSACTION_ERROR,
      payload: console.log(e.response),
    });
  }
};
