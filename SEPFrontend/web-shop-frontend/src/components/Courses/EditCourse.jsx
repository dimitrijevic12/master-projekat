import React, { Component } from "react";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import { getCourseById, editCourse } from "../../actions/actionsCourse";
import Switch from "react-switch";
import DatePicker from "react-datepicker/dist/react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

class EditCourse extends Component {
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
    startDate: "",
    endDate: "",
  };

  async componentDidMount() {
    await this.props.getCourseById(localStorage.getItem("course-id"));
    debugger;
    this.setState({
      name: this.props.course.name,
      price: this.props.course.price,
      description: this.props.course.description,
      address: this.props.course.address,
      online: this.props.course.online,
      startDate: new Date(this.props.course.startDate),
      endDate: new Date(this.props.course.endDate),
    });
  }

  render() {
    if (this.props.course === undefined) {
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
              <label for="tag">Start Date:</label>
              <div className="d-block w-100">
                <DatePicker
                  className="form-control w-100"
                  id="startDate"
                  name="startDate"
                  dateFormat="dd/MM/yyyy"
                  selected={this.state.startDate}
                  minDate={new Date()}
                  onChange={(e) => this.handleChangeStartDate(e)}
                />
              </div>
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-100 pr-5">
              <label for="location">End Date:</label>
              <div className="d-block w-100">
                <DatePicker
                  className="form-control w-100"
                  id="endDate"
                  name="endDate"
                  dateFormat="dd/MM/yyyy"
                  selected={this.state.endDate}
                  minDate={new Date()}
                  onChange={(e) => this.handleChangeEndDate(e)}
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
              this.state.name === "" ||
              this.state.startDate === "" ||
              this.state.endDate === "" ||
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

  handleChangeStartDate = (e) => {
    this.setState({
      startDate: e,
    });
  };

  handleChangeEndDate = (e) => {
    this.setState({
      endDate: e,
    });
  };

  handleChangeOnline = (checked) => {
    debugger;
    this.setState({ online: checked });
  };

  async createItem() {
    debugger;
    if (this.state.startDate > this.state.endDate) {
      toast.configure();
      toast.error("Incorrect dates!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    } else {
      const newCourse = {
        id: this.props.course.id,
        name: this.state.name,
        imagePath:
          this.state.contentPath === ""
            ? this.props.course.imagePath
            : this.state.contentPath,
        price: this.state.price,
        description: this.state.description,
        address: this.state.online === true ? "" : this.state.address,
        online: this.state.online,
        ownerId: this.props.course.ownerId,
        startDate: this.state.startDate,
        endDate: this.state.endDate,
      };
      await this.props.editCourse({
        Id: this.props.course.id,
        Name: this.state.name,
        ImagePath:
          this.state.contentPath === ""
            ? this.props.course.imagePath
            : this.state.contentPath,
        Price: this.state.price,
        Description: this.state.description,
        Address: this.state.online === true ? "" : this.state.address,
        Online: this.state.online,
        OwnerId: this.props.course.ownerId,
        StartDate: this.state.startDate,
        EndDate: this.state.endDate,
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
          if (shoppingProduct.type === "course") {
            if (newCourse.id === shoppingProduct.item.id) {
              return shoppingProduct;
            }
          }
        })
      ) {
        const elementsIndex = shoppingCartList.findIndex((element) => {
          if (element.type === "course") {
            if (element.item.id === newCourse.id) {
              return element;
            }
          }
        });
        let newArray = shoppingCartList;
        newArray[elementsIndex] = {
          ...newArray[elementsIndex],
          item: newCourse,
        };
        localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
      }
      window.location = "/courses";
    }
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
  course: state.course,
  loadedImage: state.loadedImage,
});

export default compose(connect(mapStateToProps, { getCourseById, editCourse }))(
  EditCourse
);
