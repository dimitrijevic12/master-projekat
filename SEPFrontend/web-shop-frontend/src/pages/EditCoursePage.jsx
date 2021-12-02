import React, { Component } from "react";
import EditCourse from "../components/Courses/EditCourse";
import Layout from "../layouts/Layout";

class EditCoursePage extends Component {
  render() {
    return (
      <Layout>
        <EditCourse />
      </Layout>
    );
  }
}

export default EditCoursePage;
