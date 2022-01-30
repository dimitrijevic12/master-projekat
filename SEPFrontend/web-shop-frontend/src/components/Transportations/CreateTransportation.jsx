import React, { Component } from "react";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import { createTransportation } from "../../actions/actionsTransportations";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";
import DateTimePicker from "react-datetime-picker";

class CreateTransportation extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    contentPath: "",
    name: "",
    price: "",
    description: "",
    departureTime: "",
    startDestination: "",
    finalDestination: "",
  };
  render() {
    return (
      <React.Fragment>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <input type="file" onChange={this.choosePost} />
              <img
                src={this.state.fileUrl}
                style={{ width: 500, height: 300 }}
              />
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label className="label">Name:</label>
              <input
                type="text"
                name="name"
                value={this.state.name}
                onChange={this.handleChange}
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
                  value={this.state.departureTime}
                  minDate={new Date()}
                  selected={this.state.departureTime}
                  onChange={this.handleChangeDate.bind(this)}
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
                value={this.state.startDestination}
                onChange={this.handleChange}
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
                value={this.state.finalDestination}
                onChange={this.handleChange}
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
                value={this.state.price}
                onChange={this.handleChange}
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
                value={this.state.description}
                cols="40"
                rows="5"
                onChange={this.handleChange}
                class="form-control"
                placeholder="Enter description"
              ></textarea>
            </div>
          </div>
        </div>
        <div className="mt-5 pb-5">
          <button
            onClick={() => {
              this.createTransportation();
            }}
            disabled={
              this.state.contentPath === "" ||
              this.state.name === "" ||
              this.state.price === "" ||
              this.state.departureTime === "" ||
              this.state.startDestination === "" ||
              this.state.finalDestination === ""
            }
            className="btn btn-primary btn-block"
          >
            Create
          </button>
        </div>
      </React.Fragment>
    );
  }

  handleChangeDate = (event) => {
    this.setState({
      departureTime: event,
    });
  };

  handleChangeOnline = (checked) => {
    debugger;
    this.setState({ online: checked });
  };

  async createTransportation() {
    debugger;
    var departureTime = new Date(this.state.departureTime);
    departureTime.setHours(departureTime.getHours() + 1);
    debugger;
    await this.props.createTransportation({
      Name: this.state.name,
      ImagePath: this.state.contentPath,
      DepartureTime: departureTime,
      Price: this.state.price,
      Description: this.state.description,
      StartDestination: this.state.startDestination,
      FinalDestination: this.state.finalDestination,
      OwnerId: sessionStorage.getItem("userIdWebShop"),
    });
    window.location = "/owners-transportations";
  }

  choosePost = async (event) => {
    this.setState({
      fileUrl: URL.createObjectURL(event.target.files[0]),
      fileType: event.target.files[0].type,
    });
    const formData = new FormData();

    formData.append("formFile", event.target.files[0]);
    formData.append("fileName", event.target.files[0].name);

    var dummyThis = this;
    try {
      const res = await axios({
        method: "post",
        url: `${process.env.REACT_APP_API_URL}contents`,
        data: formData,
        headers: {
          "Content-Type": "multipart/form-data",
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + sessionStorage.getItem("tokenWebShop"),
        },
      })
        .then(function (response) {
          var contentPath = { ...dummyThis.state.contentPath };
          contentPath = response.data;
          dummyThis.setState({ contentPath });
          console.log(response);
        })
        .catch(function (response) {
          console.log(response);
        });
      console.log(res);
      debugger;
      console.log(this.state.contentPath);
    } catch (ex) {
      console.log(ex);
    }
  };

  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    debugger;
    this.setState({
      [name]: value,
    });
  };
}

const mapStateToProps = (state) => ({});

export default compose(connect(mapStateToProps, { createTransportation }))(
  CreateTransportation
);
