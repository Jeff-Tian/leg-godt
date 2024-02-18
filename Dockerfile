# 使用 .NET 6.0 SDK 作为基础镜像
FROM mcr.microsoft.com/dotnet/sdk:6.0

# 安装 wget、unzip 和 fonts-liberation 以及依赖项
RUN apt-get update && \
    apt-get install -y wget \
    unzip \
    fonts-liberation \
    libasound2 \
    libatk-bridge2.0-0 \
    libatspi2.0-0 \
    libcups2 \
    libdbus-1-3 \
    libgtk-3-0 \
    libnspr4 \
    libnss3 \
    libxcomposite1 \
    libxdamage1 \
    libxkbcommon0 \
    libxrandr2 \
    xdg-utils \
    libu2f-udev \
    libvulkan1 \
    iproute2 \
    iputils-ping 

# 下载并安装 Chrome
RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb && \
    dpkg -i google-chrome-stable_current_amd64.deb; apt-get -fy install

# 清理不必要的文件和包列表以减小镜像大小
RUN rm google-chrome-stable_current_amd64.deb && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# 其他 Dockerfile 指令（如复制应用程序代码等）
# ...

# 默认命令
CMD ["bash"]