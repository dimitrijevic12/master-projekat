import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import { getTransportationById } from "../../actions/actionsTransportations";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";
import DateTimePicker from "react-datetime-picker";

class TransportationReviewModal extends Component {
  state = {
    showItemModal: this.props.show,
    quantity: 0,
  };

  async componentDidMount() {
    debugger;
    await this.props.getTransportationById(this.props.productId);
  }

  render() {
    debugger;
    if (this.props.transportation === undefined) {
      return null;
    }

    const Transportation = () => {
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
        <ModalHeader toggle={this.toggle.bind(this)}>
          Transportation
        </ModalHeader>
        <ModalBody>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <Transportation />
              </div>
            </div>
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label className="label">Name:</label>
                <input
                  type="text"
                  name="name"
                  value={this.props.transportation.name}
                  disabled={true}
                  class="form-control"
                  id="name"
                  placeholder="Enter name of product"
                />
              </div>
            </div>
          </div>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="tag">Date:</label>
                <div className="d-block w-100">
                  <DateTimePicker
                    id="datepickerInput"
                    name="start"
                    disabled={true}
                    value={this.props.transportation.departureTime}
                    minDate={new Date()}
                    selected={this.props.transportation.departureTime}
                  />
                </div>
              </div>
            </div>
          </div>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="tag">Start Destination:</label>
                <input
                  type="text"
                  name="startDestination"
                  value={this.props.transportation.startDestination}
                  disabled={true}
                  class="form-control"
                  id="startDestination"
                  placeholder="Enter start destination"
                />
              </div>
            </div>
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="location">Final Destination:</label>
                <input
                  type="text"
                  name="finalDestination"
                  value={this.props.transportation.finalDestination}
                  disabled={true}
                  class="form-control"
                  id="finalDestination"
                  placeholder="Enter final destination"
                />
              </div>
            </div>
          </div>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="tag">Price:</label>
                <input
                  type="number"
                  name="price"
                  value={this.props.transportation.price}
                  disabled={true}
                  class="form-control"
                  id="price"
                  placeholder="Enter price"
                />
              </div>
            </div>
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="location">Description:</label>
                <textarea
                  name="description"
                  value={this.props.transportation.description}
                  cols="40"
                  rows="5"
                  disabled={true}
                  class="form-control"
                  placeholder="Enter description"
                ></textarea>
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
  transportation: state.transportation,
  loadedImage: state.loadedImage,
});

export default connect(mapStateToProps, { getTransportationById })(
  TransportationReviewModal
);
