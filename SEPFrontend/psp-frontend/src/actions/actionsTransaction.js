import {
    GET_TRANSACTIONS,
    GET_TRANSACTIONS_ERROR,
  } from "../types/types";
  import axios from "axios";

  export const getTransactions = () => async (dispatch) => {
    debugger;
    try {
      const response = await axios.get(
        "https://localhost:44315/api/Transactions",
        {
          headers: {
            "Access-Control-Allow-Origin": "*",
            Authorization: "Bearer " + sessionStorage.getItem("tokenPSP"),
          },
        }
      );
      dispatch({
        type: GET_TRANSACTIONS,
        payload: response.data,
      });
    } catch (e) {
        debugger;
      dispatch({
        type: GET_TRANSACTIONS_ERROR,
        payload: console.log(e),
      });
    }
  };