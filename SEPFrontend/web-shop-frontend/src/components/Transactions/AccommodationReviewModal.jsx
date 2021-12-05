import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import { getAccommodationById } from "../../actions/actionsAccommodation";
import DatePicker from "react-datepicker/dist/react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";
import Switch from "react-switch";

class AccommodationReviewModal extends Component {
  state = {
    showItemModal: this.props.show,
    quantity: 0,
  };

  async componentDidMount() {
    debugger;
    await this.props.getAccommodationById(this.props.productId);
  }

  render() {
    debugger;
    if (this.props.accommodation === undefined) {
      return null;
    }

    const Accommodation = () => {
      debugger;
      if (this.props.loadedImage === undefined) {
        return null;
      }
      return (
        <img
          src={"data:image/jpg;base64," + this.props.loadedImage.fileContents}
          style={{ width: 300, height: 160 }}
          className="mb-3"
        />
      );
    };
    return (
      <Modal
        style={{
          maxWidth: "750px",
          width: "749px",
        }}
        isOpen={this.state.showItemModal}
        centered={true}
      >
        <ModalHeader toggle={this.toggle.bind(this)}>Conference</ModalHeader>
        <ModalBody>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <Accommodation />
              </div>
            </div>
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label className="label">Name:</label>
                <input
                  type="text"
                  name="name"
                  class="form-control"
                  id="name"
                  value={this.props.accommodation.name}
                  disabled={true}
                  placeholder="Enter name of product"
                />
              </div>
            </div>
          </div>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="tag">Cost per night:</label>
                <input
                  type="text"
                  name="price"
                  class="form-control"
                  id="price"
                  value={this.props.accommodation.costPerNight + " €"}
                  disabled={true}
                  placeholder="Enter price"
                />
              </div>
            </div>
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="location">Description:</label>
                <textarea
                  name="description"
                  value={this.props.accommodation.description}
                  cols="40"
                  rows="5"
                  class="form-control"
                  disabled={true}
                  placeholder="Enter promotion/action"
                ></textarea>
              </div>
            </div>
          </div>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="tag">Address:</label>
                <input
                  type="text"
                  name="address"
                  class="form-control"
                  id="address"
                  value={this.props.accommodation.address}
                  disabled={true}
                  placeholder="Enter address"
                />
              </div>
            </div>
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="location">City:</label>
                <input
                  type="text"
                  name="city"
                  class="form-control"
                  id="city"
                  value={this.props.accommodation.city}
                  disabled={true}
                  placeholder="Enter city"
                />
              </div>
            </div>
          </div>
        </ModalBody>
        <ModalFooter>
          <button
            className="btn btn-primary"
            onClick={() => {
              this.close();
            }}
          >
            Close
          </button>
        </ModalFooter>
      </Modal>
    );
  }

  close() {
    this.toggle();
  }

  handleChange = (event) => {
    debugger;
    const { name, value, type, checked } = event.target;
    type === "checkbox"
      ? this.setState({
          [name]: checked,
        })
      : this.setState({
          [name]: value,
        });
  };

  toggle() {
    debugger;
    this.setState({ showItemModal: false });
    this.props.onShowChange();
  }
}

const mapStateToProps = (state) => ({
  accommodation: state.accommodation,
  loadedImage: state.loadedImage,
});

export default connect(mapStateToProps, { getAccommodationById })(
  AccommodationReviewModal
);
