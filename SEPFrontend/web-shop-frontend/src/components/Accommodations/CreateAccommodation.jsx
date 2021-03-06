import React, { Component } from "react";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import { createAccommodation } from "../../actions/actionsAccommodation";

class CreateAccommodation extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    contentPath: "",
    name: "",
    price: "",
    description: "",
    address: "",
    city: "",
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
              <label for="tag">Cost per night:</label>
              <input
                type="number"
                name="price"
                value={this.state.price}
                onChange={this.handleChange}
                class="form-control"
                id="price"
                placeholder="Enter cost per night"
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
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="tag">Address:</label>
              <input
                type="text"
                name="address"
                value={this.state.address}
                onChange={this.handleChange}
                class="form-control"
                id="address"
                placeholder="Enter address"
              />
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="tag">City:</label>
              <input
                type="text"
                name="city"
                value={this.state.city}
                onChange={this.handleChange}
                class="form-control"
                id="city"
                placeholder="Enter city"
              />
            </div>
          </div>
        </div>
        <div className="mt-5 pb-5">
          <button
            onClick={() => {
              this.createAccommodation();
            }}
            disabled={
              this.state.contentPath === "" ||
              this.state.name === "" ||
              this.state.price === "" ||
              this.state.address === ""
            }
            className="btn btn-primary btn-block"
          >
            Create
          </button>
        </div>
      </React.Fragment>
    );
  }

  async createAccommodation() {
    debugger;
    await this.props.createAccommodation({
      Name: this.state.name,
      ImagePath: this.state.contentPath,
      CostPerNight: this.state.price,
      Description: this.state.description,
      Address: this.state.address,
      City: this.state.city,
      OwnerId: sessionStorage.getItem("userIdWebShop"),
    });
    window.location = "/owners-accommodations";
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

export default compose(connect(mapStateToProps, { createAccommodation }))(
  CreateAccommodation
);
