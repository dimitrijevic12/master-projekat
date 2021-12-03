import React, { Component } from "react";
import { connect } from "react-redux";
import { Table } from "reactstrap";
import { Card } from "reactstrap";
import { compose } from "redux";
import { getTransactionById } from "../../actions/actionsTransaction";
import ItemReviewModal from "./ItemReviewModal";
import ConferenceReviewModal from "./ConferenceReviewModal";
import CourseReviewModal from "./CourseReviewModal";
import AccommodationReviewModal from "./AccommodationReviewModal";
import TransportationReviewModal from "./TransportationReviewModal";

class TransactionReview extends Component {
  state = {
    showItemModal: false,
    showConferenceModal: false,
    showCourseModal: false,
    showAccommodationModal: false,
    showTransportationModal: false,
    productId: {},
  };

  async componentDidMount() {
    debugger;
    await this.props.getTransactionById(
      sessionStorage.getItem("transaction-id")
    );
  }

  render() {
    if (this.props.transaction === undefined) {
      return null;
    }
    debugger;
    return (
      <div>
        {this.state.showTransportationModal ? (
          <TransportationReviewModal
            show={this.state.showTransportationModal}
            onShowChange={this.displayModalAccommodation.bind(this)}
            productId={this.state.productId}
          />
        ) : null}
        {this.state.showAccommodationModal ? (
          <AccommodationReviewModal
            show={this.state.showAccommodationModal}
            onShowChange={this.displayModalAccommodation.bind(this)}
            productId={this.state.productId}
          />
        ) : null}
        {this.state.showCourseModal ? (
          <CourseReviewModal
            show={this.state.showCourseModal}
            onShowChange={this.displayModalCourse.bind(this)}
            productId={this.state.productId}
          />
        ) : null}
        {this.state.showItemModal ? (
          <ItemReviewModal
            show={this.state.showItemModal}
            onShowChange={this.displayModalItem.bind(this)}
            productId={this.state.productId}
          />
        ) : null}
        {this.state.showConferenceModal ? (
          <ConferenceReviewModal
            show={this.state.showConferenceModal}
            onShowChange={this.displayModalConference.bind(this)}
            productId={this.state.productId}
          />
        ) : null}
        <h3 className="mt-4" style={{ textAlign: "center" }}>
          Transaction Items
        </h3>
        <hr />
        <div className="wrap bg-white pt-3">
          <div style={{ marginTop: "40px" }} id="appointmentTable">
            <Card
              className="mt-5"
              style={{
                shadowColor: "gray",
                boxShadow: "0 8px 6px -6px #999",
              }}
            >
              <div
                style={{
                  maxHeight: "440px",
                  overflowY: "auto",
                }}
              >
                <Table className="table allPrescriptions mb-0" striped>
                  <thead>
                    <tr>
                      <th style={{ textAlign: "center" }}>Type</th>
                      <th style={{ textAlign: "center" }}>Product Id</th>
                      <th style={{ textAlign: "center" }}>Name</th>
                      <th style={{ textAlign: "center" }}>Quantity</th>
                      <th style={{ textAlign: "center" }}>Price per product</th>
                      <th style={{ textAlign: "center" }}></th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.transaction.transactionItems.map((f) => (
                      <tr>
                        <td style={{ textAlign: "center" }}>
                          {f.type === 0
                            ? "Conference"
                            : f.type === 1
                            ? "Course"
                            : f.type === 2
                            ? "Accommodation"
                            : f.type === 3
                            ? "Transportation"
                            : "Item"}
                        </td>
                        <td style={{ textAlign: "center" }}>{f.productId}</td>
                        <td style={{ textAlign: "center" }}>{f.name}</td>
                        <td style={{ textAlign: "center" }}>{f.quantity}</td>
                        <td style={{ textAlign: "center" }}>{f.price}</td>
                        <td style={{ textAlign: "center" }}>
                          <img
                            onClick={() => {
                              this.view(f);
                            }}
                            src="/images/analytics.png"
                          />
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </Table>
              </div>
            </Card>
          </div>
        </div>
      </div>
    );
  }

  view(f) {
    this.setState({ productId: f.productId });
    debugger;
    if (f.type === 0) {
      this.displayModalConference();
    } else if (f.type === 4) {
      this.displayModalItem();
    } else if (f.type === 1) {
      this.displayModalCourse();
    } else if (f.type === 2) {
      this.displayModalAccommodation();
    } else if (f.type === 3) {
      this.displayModalTransportation();
    }
  }

  displayModalTransportation() {
    this.setState({
      showTransportationModal: !this.state.showTransportationModal,
    });
  }

  displayModalAccommodation() {
    this.setState({
      showAccommodationModal: !this.state.showAccommodationModal,
    });
  }

  displayModalConference() {
    this.setState({
      showConferenceModal: !this.state.showConferenceModal,
    });
  }

  displayModalCourse() {
    this.setState({
      showCourseModal: !this.state.showCourseModal,
    });
  }

  displayModalItem() {
    this.setState({
      showItemModal: !this.state.showItemModal,
    });
  }
}

const mapStateToProps = (state) => ({
  transaction: state.transaction,
});

export default compose(connect(mapStateToProps, { getTransactionById }))(
  TransactionReview
);
