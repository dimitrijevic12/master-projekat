import React, { Component } from "react";
import { compose } from "redux";
import { connect } from "react-redux";
import axios from "axios";
import { createCourse } from "../../actions/actionsCourse";
import Switch from "react-switch";
import DatePicker from "react-datepicker/dist/react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

class CreateCourse extends Component {
  state = {
    file: null,
    fileName: "",
    fileUrl: null,
    fileType: "",
    contentPath: "",
    name: "",
    price: "",
    description: "",
    online: false,
    address: "",
    startDate: "",
    endDate: "",
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
              <label for="location">Online:</label>
              <br />
              <Switch
                onChange={this.handleChangeOnline}
                checked={this.state.online}
                className="react-switch"
                id="normal-switch"
              />
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
                disabled={this.state.online === true}
                value={this.state.online === true ? "" : this.state.address}
                onChange={this.handleChange}
                class="form-control"
                id="address"
                placeholder="Enter address"
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
              this.createConference();
            }}
            disabled={
              this.state.contentPath === "" ||
              this.state.name === "" ||
              this.state.startDate === "" ||
              this.state.endDate === "" ||
              this.state.price === "" ||
              (this.state.address === "" && this.state.online === false)
            }
            className="btn btn-primary btn-block"
          >
            Create
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

  async createConference() {
    debugger;
    if (this.state.startDate > this.state.endDate) {
      toast.configure();
      toast.error("Incorrect dates!", {
        position: toast.POSITION.TOP_RIGHT,
      });
    } else {
      await this.props.createCourse({
        Name: this.state.name,
        ImagePath: this.state.contentPath,
        Price: this.state.price,
        Description: this.state.description,
        Online: this.state.online,
        Address: this.state.online === true ? "" : this.state.address,
        StartDate: this.state.startDate,
        EndDate: this.state.endDate,
        OwnerId: sessionStorage.getItem("userIdWebShop"),
      });
      window.location = "/owners-courses";
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

export default compose(connect(mapStateToProps, { createCourse }))(
  CreateCourse
);
