import { PAY_PERDIEM, PAY_PERDIEM_ERROR } from "../types/types";
import axios from "axios";

export const payPerdiem = (perdiem) => async (dispatch) => {
  debugger;
  const perdiemDto = {
    uniquePersonalRegistrationNumber: perdiem.uniquePersonalRegistrationNumber,
    amount: perdiem.amount,
    currency: perdiem.currency,
  };
  var url =
    perdiem.bank === "IssuerBank"
      ? "https://localhost:44376/api/transactions/per-diem"
      : "https://localhost:44375/api/transactions/per-diem";
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
