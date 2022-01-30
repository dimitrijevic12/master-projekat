import React, { Component } from "react";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import {
  getTransportationById,
  editTransportation,
} from "../../actions/actionsTransportations";
import DateTimePicker from "react-datetime-picker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";

class EditTransportation extends Component {
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

  async componentDidMount() {
    await this.props.getTransportationById(
      localStorage.getItem("transportation-id")
    );
    debugger;
    this.setState({
      name: this.props.transportation.name,
      price: this.props.transportation.price,
      description: this.props.transportation.description,
      startDestination: this.props.transportation.startDestination,
      finalDestination: this.props.transportation.finalDestination,
      departureTime: new Date(this.props.transportation.departureTime),
    });
  }

  render() {
    if (this.props.transportation === undefined) {
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
              this.createItem();
            }}
            disabled={
              this.state.departureTime === "" ||
              this.state.name === "" ||
              this.state.price === "" ||
              this.state.startDestination === "" ||
              this.state.finalDestination === ""
            }
            className="btn btn-primary btn-block"
          >
            Edit
          </button>
        </div>
      </React.Fragment>
    );
  }

  handleChangeDate = (e) => {
    this.setState({
      date: e,
    });
  };

  handleChangeOnline = (checked) => {
    debugger;
    this.setState({ online: checked });
  };

  async createItem() {
    debugger;
    const newTransportation = {
      id: this.props.transportation.id,
      name: this.state.name,
      imagePath:
        this.state.contentPath === ""
          ? this.props.transportation.imagePath
          : this.state.contentPath,
      price: this.state.price,
      departureTime: this.state.departureTime,
      description: this.state.description,
      startDestination: this.state.startDestination,
      finalDestination: this.state.finalDestination,
      ownerId: this.props.transportation.ownerId,
    };
    await this.props.editTransportation({
      Id: this.props.transportation.id,
      Name: this.state.name,
      ImagePath:
        this.state.contentPath === ""
          ? this.props.transportation.imagePath
          : this.state.contentPath,
      Price: this.state.price,
      DepartureTime: this.state.departureTime,
      Description: this.state.description,
      StartDestination: this.state.startDestination,
      FinalDestination: this.state.finalDestination,
      OwnerId: this.props.transportation.ownerId,
    });
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    if (
      shoppingCartList.some((shoppingProduct) => {
        if (shoppingProduct.type === "transportation") {
          if (newTransportation.id === shoppingProduct.item.id) {
            return shoppingProduct;
          }
        }
      })
    ) {
      const elementsIndex = shoppingCartList.findIndex((element) => {
        if (element.type === "transportation") {
          if (element.item.id === newTransportation.id) {
            return element;
          }
        }
      });
      let newArray = shoppingCartList;
      newArray[elementsIndex] = {
        ...newArray[elementsIndex],
        item: newTransportation,
      };
      localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    }
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

const mapStateToProps = (state) => ({
  transportation: state.transportation,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, { getTransportationById, editTransportation })
)(EditTransportation);
