import React, { useEffect, useState } from "react";
import { getConferenceById } from "../../actions/actionsConference";
import { connect } from "react-redux";
import { compose } from "redux";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Switch from "react-switch";
import BuyConferenceModal from "./BuyConferenceModal";
import DatePicker from "react-datepicker/dist/react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "../../css/datepicker.css";

function ReviewConference(props) {
  const [item, setItem] = useState({});
  const [showPostModal, setShowPostModal] = useState(false);

  useEffect(() => {
    debugger;
    props.getConferenceById(localStorage.getItem("shoppingItem-id"));
  }, []);

  const Conference = () => {
    debugger;
    if (props.loadedImage === undefined) {
      return null;
    }
    return (
      <img
        src={"data:image/jpg;base64," + props.loadedImage.fileContents}
        style={{ width: 600, height: 320 }}
        className="mb-3"
      />
    );
  };

  const displayModalPost = (item) => {
    if (item != undefined) {
      setItem(item);
    }
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    shoppingCartList = shoppingCartList.filter((cartItem) => {
      if (cartItem.type === "conference") {
        return cartItem.item.id !== item.id;
      } else {
        return cartItem;
      }
    });
    localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    window.location = "/items-in-shopping-cart";
  };

  if (props.conference === undefined) {
    return null;
  }

  return (
    <React.Fragment>
      {showPostModal ? (
        <BuyConferenceModal
          show={showPostModal}
          item={item}
          onShowChange={() => displayModalPost()}
        />
      ) : null}
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
              value={props.conference.name}
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
              checked={props.conference.online}
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
              value={props.conference.address}
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
                selected={new Date(props.conference.date)}
                disabled={true}
                minDate={new Date()}
              />
            </div>
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Quantity:</label>
            <input
              type="number"
              name="quantity"
              class="form-control"
              id="quantity"
              value={localStorage.getItem("quantity-for-shopping-item")}
              disabled={true}
              placeholder="Enter quantity"
            />
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
              value={props.conference.price + " €"}
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
              value={props.conference.description}
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
            <label for="location">Total price:</label>
            <input
              type="text"
              name="quantity"
              class="form-control"
              id="quantity"
              value={
                parseInt(localStorage.getItem("quantity-for-shopping-item")) *
                  parseInt(props.conference.price) +
                " €"
              }
              disabled={true}
              placeholder="Enter quantity"
            />
          </div>
        </div>
      </div>
      <div style={{ textAlign: "center" }} className="mt-5 pb-5">
        <button
          onClick={() => displayModalPost(props.conference)}
          className="btn btn-primary"
        >
          Remove from shopping cart
        </button>
      </div>
    </React.Fragment>
  );
}

const mapStateToProps = (state) => ({
  conference: state.conference,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, {
    getConferenceById,
  })
)(ReviewConference);
