import React, { Component } from "react";
import PayPalButtonSubscription from "../components/PayPal/PayPalButtonSubscription";
import Layout from "../layouts/Layout";

class PayPalButtonSubscriptionPage extends Component {
  render() {
    return (
      <Layout>
        <main
          className="main d-flex pt-0 pb-0 text-center justify-content-center align-items-center"
          style={{ height: "100vh", backgroundColor: "#82b0fa" }}
        >
          <div className="wrap bg-white p-5 w-50">
            <div className="text-center">
              <div className="d-inline-flex w-50">
                <div class="form-group w-100 pr-5">
                  <PayPalButtonSubscription />
                </div>
              </div>
            </div>
          </div>
        </main>
      </Layout>
    );
  }
}

export default PayPalButtonSubscriptionPage;
