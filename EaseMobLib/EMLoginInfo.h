#pragma once
#include "EMUtils.h"
namespace EaseMobLib {
	public ref class EMLoginInfo
	{
	public:
		EMLoginInfo(const std::string username, const std::string password, const std::string token) {
			_username = getCSString(username);
			_password = getCSString(password);
			_token = getCSString(token);
		}
		property String^ loginUser {
			String^ get() {
				return _username;
			}
		}
		property String^ loginPassword {
			String^ get() {
				return _password;
			}
		}
		property String^ loginToken {
			String^ get() {
				return _token;
			}
		}
	private:
		String^ _username;
		String^ _password;
		String^ _token;
	};
}

