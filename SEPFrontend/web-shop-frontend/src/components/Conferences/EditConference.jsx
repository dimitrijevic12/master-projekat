import React, { Component } from "react";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import {
  getConferenceById,
  editConference,
} from "../../actions/actionsConference";
import Switch from "react-switch";

class EditConference extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    contentPath: "",
    name: "",
    price: "",
    address: "",
    online: "",
    description: "",
  };

  async componentDidMount() {
    await this.props.getConferenceById(localStorage.getItem("conference-id"));
    debugger;
    this.setState({
      name: this.props.conference.name,
      price: this.props.conference.price,
      description: this.props.conference.description,
      address: this.props.conference.address,
      online: this.props.conference.online,
    });
  }

  render() {
    if (this.props.conference === undefined) {
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
              <label for="tag">Online:</label>
              <br />
              <Switch
                onChange={this.handleChangeOnline}
                checked={this.state.online}
                className="react-switch"
                id="normal-switch"
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
                disabled={this.state.online === true}
                value={this.state.online === true ? "" : this.state.address}
                onChange={this.handleChange}
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
                class="form-control"
                onChange={this.handleChange}
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
              this.state.contentPath === "" ||
              this.state.name === "" ||
              this.state.price === "" ||
              (this.state.address === "" && this.state.online === false)
            }
            className="btn btn-primary btn-block"
          >
            Edit
          </button>
        </div>
      </React.Fragment>
    );
  }

  handleChangeOnline = (checked) => {
    debugger;
    this.setState({ online: checked });
  };

  async createItem() {
    debugger;
    const newConference = {
      id: this.props.conference.id,
      name: this.state.name,
      imagePath:
        this.state.contentPath === ""
          ? this.props.conference.imagePath
          : this.state.contentPath,
      price: this.state.price,
      description: this.state.description,
      address: this.state.online === true ? "" : this.state.address,
      online: this.state.online,
      ownerId: this.props.conference.ownerId,
    };
    await this.props.editConference({
      Id: this.props.conference.id,
      Name: this.state.name,
      ImagePath:
        this.state.contentPath === ""
          ? this.props.conference.imagePath
          : this.state.contentPath,
      Price: this.state.price,
      Description: this.state.description,
      Address: this.state.online === true ? "" : this.state.address,
      Online: this.state.online,
      OwnerId: this.props.conference.ownerId,
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
    //   shoppingCartList.some(
    //     (shoppingProduct) =>
    //       newItem.productKey === shoppingProduct.item.productKey
    //   )
    // ) {
    //   const elementsIndex = shoppingCartList.findIndex(
    //     (element) => element.item.productKey == newItem.productKey
    //   );
    //   let newArray = shoppingCartList;
    //   newArray[elementsIndex] = {
    //     ...newArray[elementsIndex],
    //     item: newItem,
    //   };
    //   localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    // }
    window.location = "/conferences";
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
        url: "https://localhost:5001/api/contents",
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
  conference: state.conference,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, { getConferenceById, editConference })
)(EditConference);
