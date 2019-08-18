npm run build-prod --prefix ../MyCommunity/MyCommunity-UI
mkdir -p ./build/MyCommunity-UI
cp -r ../MyCommunity/MyCommunity-UI/dist/MyCommunity-UI/* ./build/MyCommunity-UI
docker build -t mycommunity-ui -f 01-d-UI .