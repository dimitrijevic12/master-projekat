import React, { Component } from "react";
import CardPayment from "../components/CardPayment";
import QRPayment from "../components/QRPayment";
import Layout from "../layouts/Layout";

function QRCodePaymentPage() {
  return (
    <Layout>
      <QRPayment />
    </Layout>
  );
}

export default QRCodePaymentPage;
