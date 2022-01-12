import {
    SET_CRYPTO_TRANSACTION_STATUS,
    SET_CRYPTO_TRANSACTION_STATUS_ERROR,
    GET_CRYPTO_TRANSACTION,
    GET_CRYPTO_TRANSACTION_ERROR,
    PAY_WITH_CRYPTO_VALUTE,
    PAY_WITH_CRYPTO_VALUTE_ERROR
  } from "../types/types";
  import axios from "axios";
  
  export const setTransactionStatus = (transactionStatus) => async (dispatch) => {
      debugger;
      try {
        const response = await axios.put(
          "https://localhost:44390/api/crypto-transactions",
          transactionStatus,
          {
            headers: {
              "Access-Control-Allow-Origin": "*",
            },
          }
        );
        dispatch({
          type: SET_CRYPTO_TRANSACTION_STATUS,
          payload: response.data,
        });
        return true;
      } catch (e) {
        dispatch({
          type: SET_CRYPTO_TRANSACTION_STATUS_ERROR,
          payload: console.log(e),
        });
      }
    };
  
    export const payWithCryptoValute = (transaction) => async (dispatch) => {
        debugger;
        try {
          const response = await axios.post(
            "https://localhost:44390/api/transactions/cryptoPayment",
            transaction,
            {
              headers: {
                "Access-Control-Allow-Origin": "*",
              },
            }
          );
          debugger;
          dispatch({
            type: PAY_WITH_CRYPTO_VALUTE,
            payload: response.data,
          });
          debugger;
          return true;
        } catch (e) {
          dispatch({
            type: PAY_WITH_CRYPTO_VALUTE_ERROR,
            payload: console.log(e),
          });
        }
      };

      export const getCrpytoTransaction = (orderId) => async (dispatch) => {
        debugger;
        try {
          const response = await axios.get(
            "https://localhost:44390/api/crypto-transactions/" + orderId,
            {
              headers: {
                "Access-Control-Allow-Origin": "*",
                Authorization: "Bearer " + sessionStorage.getItem("tokenPSP"),
              },
            }
          );
          dispatch({
            type: GET_CRYPTO_TRANSACTION,
            payload: response.data,
          });
        } catch (e) {
          debugger;
          dispatch({
            type: GET_CRYPTO_TRANSACTION_ERROR,
            payload: console.log(e),
          });
        }
      };