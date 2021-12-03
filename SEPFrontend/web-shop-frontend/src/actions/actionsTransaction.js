import {
  SAVE_TRANSACTION,
  SAVE_TRANSACTION_ERROR,
  GET_TRANSACTIONS_FOR_BUYER,
  GET_TRANSACTIONS_FOR_BUYER_ERROR,
  GET_TRANSACTION_BY_ID,
  GET_TRANSACTION_BY_ID_ERROR,
  GET_TRANSACTIONS_FOR_SELLER,
  GET_TRANSACTIONS_FOR_SELLER_ERROR,
  EDIT_TRANSPORTATION,
  EDIT_TRANSPORTATION_ERROR,
} from "../types/types";
import axios from "axios";

export const saveTransaction = (transaction) => async (dispatch) => {
  try {
    const response = await axios.post(
      "https://localhost:44326/api/transactions",
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

export const getTransactionsForBuyer = (userId) => async (dispatch) => {
  userId = sessionStorage.getItem("userIdWebShop");
  try {
    const response = await axios.get(
      `https://localhost:44326/api/transactions/buyers/` + userId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_TRANSACTIONS_FOR_BUYER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_TRANSACTIONS_FOR_BUYER_ERROR,
      payload: console.log(e),
    });
  }
};

export const getTransactionById = (id) => async (dispatch) => {
  try {
    const response = await axios.get(
      `https://localhost:44326/api/transactions/` + id,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_TRANSACTION_BY_ID,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_TRANSACTION_BY_ID_ERROR,
      payload: console.log(e),
    });
  }
};

export const getTransactionsForSeller = (userId) => async (dispatch) => {
  userId = sessionStorage.getItem("userIdWebShop");
  try {
    const response = await axios.get(
      `https://localhost:44326/api/transactions/sellers/` + userId,
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      }
    );
    dispatch({
      type: GET_TRANSACTIONS_FOR_SELLER,
      payload: response.data,
    });
  } catch (e) {
    dispatch({
      type: GET_TRANSACTIONS_FOR_SELLER_ERROR,
      payload: console.log(e),
    });
  }
};
