import React, { Component } from "react";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import {
  getAccommodationById,
  editAccommodation,
} from "../../actions/actionsAccommodation";

class EditAccommodation extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    contentPath: "",
    name: "",
    costPerNight: "",
    availableCount: "",
    description: "",
    address: "",
    city: "",
  };

  async componentDidMount() {
    await this.props.getAccommodationById(
      localStorage.getItem("accommodation-id")
    );
    debugger;
    this.setState({
      name: this.props.accommodation.name,
      costPerNight: this.props.accommodation.costPerNight,
      description: this.props.accommodation.description,
      address: this.props.accommodation.address,
      city: this.props.accommodation.city,
    });
  }

  render() {
    if (this.props.accommodation === undefined) {
      return null;
    }

    if (this.props.loadedImage === undefined) {
      return null;
    }
    return (
      <React.Fragment>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <img
                src={
                  "data:image/jpg;base64," + this.props.loadedImage.fileContents
                }
                style={{ width: 600, height: 320 }}
                className="mb-3"
              />
            </div>
          </div>
        </div>
        <div className="mt-5">
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label>You can choose new photo if you want to: </label>
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
                name="costPerNight"
                value={this.state.costPerNight}
                onChange={this.handleChange}
                class="form-control"
                id="costPerNight"
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
              this.state.name === "" ||
              this.state.costPerNight === "" ||
              this.state.address === ""
            }
            className="btn btn-primary btn-block"
          >
            Edit
          </button>
        </div>
      </React.Fragment>
    );
  }

  async createAccommodation() {
    debugger;
    const newItem = {
      id: this.props.accommodation.id,
      name: this.state.name,
      imagePath:
        this.state.contentPath === ""
          ? this.props.accommodation.imagePath
          : this.state.contentPath,
      costPerNight: this.state.costPerNight,
      description: this.state.description,
      address: this.state.address,
      city: this.state.city,
      ownerId: this.props.accommodation.ownerId,
    };
    await this.props.editAccommodation({
      Id: this.props.accommodation.id,
      Name: this.state.name,
      ImagePath:
        this.state.contentPath === ""
          ? this.props.accommodation.imagePath
          : this.state.contentPath,
      CostPerNight: this.state.costPerNight,
      Description: this.state.description,
      Address: this.state.address,
      City: this.state.city,
      OwnerId: this.props.accommodation.ownerId,
    });
    // if (
    //   localStorage.getItem("shoppingCart") === null ||
    //   localStorage.getItem("shoppingCart") === ""
    // ) {
    //   var shoppingCartList = [];
    // } else {
    //   var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    // }
    // if (
    //   shoppingCartList.some((shoppingProduct) => {
    //     if (shoppingProduct.type === "accommodation") {
    //       if (newItem.id === shoppingProduct.item.id) {
    //         return shoppingProduct;
    //       }
    //     }
    //   })
    // ) {
    //   const elementsIndex = shoppingCartList.findIndex((element) => {
    //     if (element.type === "accommodation") {
    //       if (element.item.id === newItem.id) {
    //         return element;
    //       }
    //     }
    //   });
    //   let newArray = shoppingCartList;
    //   newArray[elementsIndex] = {
    //     ...newArray[elementsIndex],
    //     item: newItem,
    //   };
    //   localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    // }
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

const mapStateToProps = (state) => ({
  accommodation: state.accommodation,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, { getAccommodationById, editAccommodation })
)(EditAccommodation);
