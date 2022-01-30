import React from "react";

function PaymentType() {
  return (
    <main
      className="main d-flex pt-0 pb-0 text-center justify-content-center align-items-center"
      style={{ height: "100vh", backgroundColor: "#82b0fa" }}
    >
      <div className="wrap bg-white p-5 w-50">
        <div className="w-100">
          <button
            className="btn btn-lg btn-primary btn-block w-75 pt-3 pb-3"
            style={{ height: "80px" }}
            onClick={() => {
              window.location.href = `${process.env.REACT_APP_URL}cardpayment/${window.location.pathname.slice(
                -36
              )}`;
            }}
          >
            <img
              src="/credit-card-white.png"
              height="45px"
              width="45px"
              style={{ marginRight: "30px", float: "left", marginTop: "-5px" }}
            />
            <span className="mt-5">Card payment</span>
          </button>
        </div>
        <div className="pt-5">
          <button
            className="btn btn-lg btn-primary btn-block w-75 pt-3 pb-3"
            style={{ height: "80px" }}
            onClick={() =>
              (window.location.href = `${process.env.REACT_APP_URL}qrpayment/${window.location.pathname.slice(
                -36
              )}`)
            }
          >
            <span>
              <img
                src="/qr-white.png"
                height="45px"
                width="45px"
                style={{ marginRight: "30px", float: "left" }}
              />
              <div style={{ marginTop: "5px" }}>QR code payment</div>
            </span>
          </button>
        </div>
      </div>
    </main>
  );
}

export default PaymentType;
