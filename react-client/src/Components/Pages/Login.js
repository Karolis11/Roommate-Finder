import React from 'react';
import { useContext } from 'react';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { useSnackbar } from 'notistack';
import { useNavigate } from 'react-router-dom';
import AuthContext from "../../Contexts/AuthProvider";
import setAuthToken from "../../Contexts/SetAuthToken";

import '../Views/CenteredForm.css';

const LOGIN_URL = '/auth';

export const Login = (props) => {
    const navigate = useNavigate();
    
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    const { setAuth } = useContext(AuthContext);


    const formik = useFormik({
        initialValues: {
            email: "",
            password: "",
            firstName: "",
            lastName: "",
            city: ""
        },
        validationSchema: Yup.object({
            email: Yup
                        .string()
                        .required("Required"),
            password: Yup
                        .string()
                        .required("Required"),
        }),
        onSubmit: (values) => {
            axios({
                method: 'post',
                url: 'https://localhost:44332/login',
                data: values
            })
            .then((response) => {
                if(response.data.isSuccess){ 
                    enqueueSnackbar(response.data.message, { variant: "success" });
                    const accessToken = response?.data?.accessToken;
                    const email = response?.data?.email;
                    setAuth({ email, accessToken });
                    localStorage.setItem('token', accessToken);
                    localStorage.getItem('token');
                    setAuthToken(accessToken);
                    console.log(accessToken);
                    navigate(`/dashboard`);
                }
                else{
                    enqueueSnackbar(response.data.message, { variant: "error" });
                }
                               
            })
        }
    })

    return (
        <>
        <div className="centered-container login-container">
            <form onSubmit={formik.handleSubmit}>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="email">Email</label></div>
                    <div className="form-field-flex">
                        <input 
                            name="email" 
                            id="email" 
                            type="email"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.email}
                        />
                        { 
                            formik.touched.email && formik.errors.email &&
                                <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                    {formik.errors.email}
                                </p> 
                        }
                    </div>
                </div>
                <div className="form-field-container-flex">
                    <div className="form-field-flex"><label htmlFor="password">Password</label></div>
                    <div className="form-field-flex">
                        <input 
                            name="password" 
                            id="password" 
                            type="password"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.password}
                        />
                        { 
                            formik.touched.password && formik.errors.password &&
                                <p style={{color: "red", margin:"0", padding: "0", fontSize: "10px"}}>
                                    {formik.errors.email}
                                </p>  
                        }
                    </div>
                </div>
                <button type="submit">Submit</button>
            </form>          
        </div>
        </>
    )
}