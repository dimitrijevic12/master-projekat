import { PAY_PERDIEM, PAY_PERDIEM_ERROR } from "../types/types";
import axios from "axios";

export const payPerdiem = (perdiem) => async (dispatch) => {
  debugger;
  const perdiemDto = {
    uniquePersonalRegistrationNumber: perdiem.uniquePersonalRegistrationNumber,
    amount: perdiem.amount,
    currency: perdiem.currency,
    accountNumber: perdiem.accountNumber,
    transactionId: perdiem.transactionId,
  };
  var url =
    perdiem.bank === "Erste Bank"
      ? `${process.env.REACT_APP_ISSUER_BANK_APP_API_URL}transactions/per-diem`
      : `${process.env.REACT_APP_BANK_APP_API_URL}transactions/per-diem`;
  try {
    const response = await axios.post(url, perdiemDto, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
      },
    });
    dispatch({
      type: PAY_PERDIEM,
      payload: response.data,
    });
    return true;
  } catch (e) {
    dispatch({
      type: PAY_PERDIEM_ERROR,
      payload: console.log(e),
    });
  }
};
