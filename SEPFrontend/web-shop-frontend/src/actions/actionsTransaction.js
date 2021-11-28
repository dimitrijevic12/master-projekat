import { SAVE_TRANSACTION, SAVE_TRANSACTION_ERROR } from "../types/types";
import axios from "axios";

export const saveTransaction = (transaction) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:5001/api/transactions",
      transaction,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: SAVE_TRANSACTION,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: SAVE_TRANSACTION_ERROR,
      payload: console.log(e),
    });
  }
};
