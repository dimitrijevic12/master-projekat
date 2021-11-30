import {
    REGISTER_WEBSHOP,
    REGISTER_WEBSHOP_ERROR,
  } from "../types/types";
  import axios from "axios";

export const webShopRegistration = (webShop) => async (dispatch) => {
    debugger;
    try {
        const response = await axios.post(
        "https://localhost:44315/api/RegisteredWebShops",
        webShop,
        {
            headers: {
            "Access-Control-Allow-Origin": "*",
            },
        }
        );
        dispatch({
        type: REGISTER_WEBSHOP,
        payload: response.data,
        });
        return true;
    } catch (e) {
        dispatch({
        type: REGISTER_WEBSHOP_ERROR,
        payload: console.log(e),
        });
    }
};
