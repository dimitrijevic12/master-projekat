import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { connect } from "react-redux";
import { getConferenceById } from "../../actions/actionsConference";
import DatePicker from "react-datepicker/dist/react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";
import Switch from "react-switch";

class ConferenceReviewModal extends Component {
  state = {
    showItemModal: this.props.show,
    quantity: 0,
  };

  async componentDidMount() {
    debugger;
    await this.props.getConferenceById(this.props.productId);
  }

  render() {
    debugger;
    if (this.props.conference === undefined) {
      return null;
    }

    const Conference = () => {
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
                <Conference />
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
                  value={this.props.conference.name}
                  disabled={true}
                  placeholder="Enter name of product"
                />
              </div>
            </div>
          </div>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="tag">Online:</label>
                <br />
                <Switch
                  checked={this.props.conference.online}
                  className="react-switch"
                  id="normal-switch"
                  disabled={true}
                />
              </div>
            </div>
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="location">Address:</label>
                <input
                  type="text"
                  name="address"
                  class="form-control"
                  id="address"
                  value={this.props.conference.address}
                  disabled={true}
                />
              </div>
            </div>
          </div>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="tag">Date:</label>
                <div className="d-block w-100">
                  <DatePicker
                    className="form-control w-100"
                    id="date"
                    name="date"
                    dateFormat="dd/MM/yyyy"
                    selected={new Date(this.props.conference.date)}
                    disabled={true}
                    minDate={new Date()}
                  />
                </div>
              </div>
            </div>
          </div>
          <div className="mt-5">
            <div className="d-inline-flex w-50">
              <div class="form-group w-100 pr-5">
                <label for="tag">Price:</label>
                <input
                  type="text"
                  name="price"
                  class="form-control"
                  id="price"
                  value={this.props.conference.price + " €"}
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
                  value={this.props.conference.description}
                  cols="40"
                  rows="5"
                  class="form-control"
                  disabled={true}
                  placeholder="Enter promotion/action"
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
  conference: state.conference,
  loadedImage: state.loadedImage,
});

export default connect(mapStateToProps, { getConferenceById })(
  ConferenceReviewModal
);
