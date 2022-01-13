import { compose } from "redux";
import { useEffect } from "react";
import { connect } from "react-redux";
import React from "react";
import { getTransactionStatus } from "../actions/actions";
import { useSelector, useDispatch } from "react-redux";

export function Pending() {
  const successUrl = useSelector((state) => state.successUrl);
  const errorUrl = useSelector((state) => state.errorUrl);
  const failedUrl = useSelector((state) => state.failedUrl);

  const dispatch = useDispatch();

  useEffect(() => {
    debugger;
    GetTransactionStatus();
  }, [successUrl]);

  async function GetTransactionStatus() {
    debugger;
    let pending = await dispatch(
      getTransactionStatus(
        window.location.pathname.slice(-36),
        successUrl,
        errorUrl,
        failedUrl
      )
    );
    if (pending) {
      await new Promise((r) => setTimeout(r, 1000));
      GetTransactionStatus();
    }
    return;
  }

  return <div>Pending...</div>;
}

export default Pending;
