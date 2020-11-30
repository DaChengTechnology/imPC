#pragma once
using namespace System;

#pragma managed\

namespace EaseMobLib {
	public ref class EMBase {
	public:
		template<typename T>
		T& getNative() {
			return *(T*)mImpl;
		}

	public:
		template<typename T>
		void nativeInit() {
			nativeInit<T>(nullptr);
		}

		template<typename T>
		void setNativeHandler(const void* p) {
			T* old = (T*)this->mImpl;
			if (old != nullptr) {
				delete old;
				old = nullptr;
			}
			T* ptr = (T*)p;
			if (ptr == nullptr) {
				return;
			}
			this->mImpl = new T(*ptr);
		}

		template<typename T>
		void nativeInit(const EMBase^ ref) {
			T* old = (T*)this->mImpl;
			if (old != nullptr) {
				delete old;
				old = nullptr;
			}
			if (ref == nullptr) {
				return;
			}
			T* ptr = (T*)ref->mImpl;
			if (ptr == nullptr) {
				return;
			}
			this->mImpl = new T(*ptr);
		}

		template<typename T>
		void nativeFinalize() {
			T* ptr = (T*)this->mImpl;
			if (ptr != nullptr) {
				delete ptr;
			}
			this->mImpl = nullptr;
		}

	private:
		void* mImpl;
	};
}