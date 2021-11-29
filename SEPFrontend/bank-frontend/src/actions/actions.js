import { GET_PSPREQUEST, GET_PSPREQUEST_ERROR } from "../types/types";
import axios from "axios";

export const getPSPRequest = (paymentId) => async (dispatch) => {
  try {
    debugger;
    const response = await axios.get("https://localhost:5001/api/psprequests", {
      params: { paymentId: paymentId },

      headers: {
        "Access-Control-Allow-Origin": "*",
        //Authorization: "Bearer " + sessionStorage.getItem("tokenAgentApp"),
      },
    });
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
