<template>
  <div class="login" clearfix>
    <div class="login-wrap">
      <el-row type="flex" justify="center">
        <el-form ref="loginForm" :model="user" :rules="rules" status-icon label-width="80px">
          <h3>忘记密码</h3>
          <hr>
            <el-form-item prop="username" label="用户名">
                <el-input v-model="user.username" placeholder="请输入用户名"></el-input>
            </el-form-item>
            <el-form-item prop="email" label="邮箱">
                 <el-input v-model="user.email" placeholder="请输入邮箱"></el-input>
            </el-form-item>
            <el-form-item prop="password" label="设置密码">
                 <el-input v-model="user.password" show-password placeholder="请输入密码"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" icon @click="doRegister()">确 认</el-button>
          </el-form-item>
        </el-form>
      </el-row>
    </div>
  </div>
</template>
 
<script>

export default {
  name: "ForgetPwd",
  data() {
    return {
      user: {
        username: "",
        password: ""
      }  
    };
  },
  created() {},
  methods: {
    doLogin() {

    }
  }
};
</script>
 
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.login {
  width: 100%;
  height: 710px;
  background: url("../../assets/images/login_bg.jpg") no-repeat;
  background-size: cover;
  overflow: hidden;
}
.login-wrap {
  background: #fff;
  background-size: cover;
  width: 400px;
  margin: 215px auto;
  overflow: hidden;
  padding-top: 10px;
  line-height: 40px;
  border-radius: 10px;
  box-shadow: 0 2px 10px 0 rgb(131 121 247 / 26%);
}
#password {
  margin-bottom: 5px;
}
h3 {
  color: #0babeab8;
  font-size: 24px;
}
hr {
  background-color: #444;
  margin: 20px auto;
}
a {
  text-decoration: none;
  color: #606266;
  font-size: 15px;
}
a:hover {
  color: coral;
}
.el-button {
  width: 100%;
}

.forgetPwd{
  margin-left: 24%;
}
.register{
  margin-left: 35%;
}
</style>