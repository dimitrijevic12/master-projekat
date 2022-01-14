import React, { Component } from "react";
import CardPayment from "../components/CardPayment";
import Pending from "../components/Pending";
import Layout from "../layouts/Layout";

function PendingPage() {
  return (
    <Layout>
      <Pending />
    </Layout>
  );
}

export default PendingPage;
